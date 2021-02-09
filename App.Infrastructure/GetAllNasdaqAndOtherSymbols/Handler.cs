// <copyright file="Handler.cs" company="None">
// Free and open source code.
// </copyright>
namespace App.Infrastructure.GetAllNasdaqAndOtherSymbols
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using CsvHelper;
    using CsvHelper.Configuration;

    /// <summary>
    /// The handler that process the get all NASDAQ symbols class.
    /// </summary>
    public class Handler
    {
        private const string FileCreationTimeText = @"File Creation Time:";
        private const string NasdaqListedUri = @"http://www.nasdaqtrader.com/dynamic/SymDir/nasdaqlisted.txt";
        private const string OtherListedUri = @"http://www.nasdaqtrader.com/dynamic/SymDir/otherlisted.txt";

        private static readonly CsvConfiguration CsvConfigurationPipe =
            new CsvConfiguration(
                cultureInfo: CultureInfo.InvariantCulture,
                delimiter: "|");

        private readonly IHttpClientFactory clientFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="Handler"/> class.
        /// </summary>
        /// <param name="clientFactory">IHttpClientFactory.</param>
        public Handler(IHttpClientFactory clientFactory)
        {
            this.clientFactory = clientFactory;
        }

        /// <summary>
        /// Get all NASDAQ and other symbols handler.
        /// </summary>
        /// <param name="request">The query.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>String.</returns>
        public async Task<Result> Handle(Query request, CancellationToken cancellationToken)
        {
            var nasdaqSymbolTask = this.GetItemsAsync<NasdaqSymbol>(
                uri: NasdaqListedUri,
                csvConfiguration: CsvConfigurationPipe,
                cancellationToken: cancellationToken,
                createItem: (csv) =>
                {
                    return new NasdaqSymbol
                    {
                        Symbol = csv[0].Trim(),
                        SecurityName = csv[1].Trim(),
                        MarketCategory = csv.GetField<char>(2),
                        TestIssue = Parse.Boolean(csv[3]),
                        FinancialStatus = csv.GetField<char>(4),
                        RoundLotSize = csv.GetField<int>(5),
                        ETF = Parse.Boolean(csv[6]),
                        NextShares = Parse.Boolean(csv[7]),
                    };
                });

            var otherSymbolTask = this.GetItemsAsync<OtherSymbol>(
                uri: OtherListedUri,
                csvConfiguration: CsvConfigurationPipe,
                cancellationToken: cancellationToken,
                createItem: (csv) =>
                {
                    return new OtherSymbol
                    {
                        ActSymbol = csv[0].Trim(),
                        SecurityName = csv[1].Trim(),
                        Exchange = csv.GetField<char>(2),
                        CqsSymbol = csv[3].Trim(),
                        ETF = Parse.Boolean(csv[4]),
                        RoundLotSize = csv.GetField<int>(5),
                        TestIssue = Parse.Boolean(csv[6]),
                        NASDAQSymbol = csv[7].Trim(),
                    };
                });

            await Task.WhenAll(nasdaqSymbolTask, otherSymbolTask);

            return new Result
            {
                NasdaqSymbols = nasdaqSymbolTask.Result.Item1,
                NasdaqSymbolsFileCreationTime = nasdaqSymbolTask.Result.Item2,
                OtherSymbols = otherSymbolTask.Result.Item1,
                OtherSymbolsFileCreationTime = otherSymbolTask.Result.Item2,
            };
        }

        /// <summary>
        /// Get a list of items.
        /// </summary>
        /// <typeparam name="Titem">Item to create.</typeparam>
        /// <param name="uri">URL to get the data.</param>
        /// <param name="csvConfiguration">CSV configuration.</param>
        /// <param name="createItem">Function to create item.</param>
        /// <param name="cancellationToken">CancellationToken.</param>
        /// <returns>List of items.</returns>
        private async Task<Tuple<IEnumerable<Titem>, DateTime>> GetItemsAsync<Titem>(
            string uri,
            CsvConfiguration csvConfiguration,
            Func<CsvReader, Titem> createItem,
            CancellationToken cancellationToken)
        {
            List<Titem> items = new List<Titem>();
            DateTime fileCreationTime = default;

            HttpClient httpClient = this.clientFactory.CreateClient();
            using var responseStream = await httpClient.GetStreamAsync(uri, cancellationToken);
            using var streamReader = new StreamReader(responseStream);

            using CsvReader csv = new CsvReader(streamReader, csvConfiguration);

            if (!cancellationToken.IsCancellationRequested && await csv.ReadAsync())
            {
                csv.ReadHeader();

                while (!cancellationToken.IsCancellationRequested && await csv.ReadAsync())
                {
                    if (csv[0].StartsWith(FileCreationTimeText))
                    {
                        fileCreationTime = Parse.FileCreationTime(csv[0][FileCreationTimeText.Length..]);
                    }
                    else
                    {
                        items.Add(createItem(csv));
                    }
                }
            }

            return new Tuple<IEnumerable<Titem>, DateTime>(items, fileCreationTime);
        }
    }
}