// <copyright file="GetAllSymbolsNasdaqHandler.cs" company="None">
// Free and open source code.
// </copyright>
namespace App.Infrastructure.Handlers
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using App.Infrastructure.Dtos;
    using CsvHelper;
    using CsvHelper.Configuration;
    using MediatR;

    /// <summary>
    /// The handler that process the get all NASDAQ symbols class.
    /// </summary>
    public class GetAllSymbolsNasdaqHandler : IRequestHandler<GetAllSymbolsNasdaq.Query, GetAllSymbolsNasdaq.Result>
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
        /// Initializes a new instance of the <see cref="GetAllSymbolsNasdaqHandler"/> class.
        /// </summary>
        /// <param name="clientFactory">IHttpClientFactory.</param>
        public GetAllSymbolsNasdaqHandler(IHttpClientFactory clientFactory)
        {
            this.clientFactory = clientFactory;
        }

        /// <summary>
        /// Get all NASDAQ and other symbols handler.
        /// </summary>
        /// <param name="request">The query.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>String.</returns>
        public async Task<GetAllSymbolsNasdaq.Result> Handle(GetAllSymbolsNasdaq.Query request, CancellationToken cancellationToken)
        {
            var nasdaqSymbolTask = this.GetItemsAsync<NasdaqSymbol>(
                uri: NasdaqListedUri,
                csvConfiguration: CsvConfigurationPipe,
                cancellationToken: cancellationToken,
                createItem: (csv) =>
                {
                    return new NasdaqSymbol(
                        symbol: csv[0].Trim(),
                        securityName: csv[1].Trim(),
                        marketCategory: csv.GetField<char>(2),
                        testIssue: Parse.Boolean(csv[3]),
                        financialStatus: csv.GetField<char>(4),
                        roundLotSize: csv.GetField<int>(5),
                        etf: Parse.Boolean(csv[6]),
                        nextShares: Parse.Boolean(csv[7]));
                });

            var otherSymbolTask = this.GetItemsAsync<OtherSymbol>(
                uri: OtherListedUri,
                csvConfiguration: CsvConfigurationPipe,
                cancellationToken: cancellationToken,
                createItem: (csv) =>
                {
                    return new OtherSymbol(
                        actSymbol: csv[0].Trim(),
                        securityName: csv[1].Trim(),
                        exchange: csv.GetField<char>(2),
                        cqsSymbol: csv[3].Trim(),
                        etf: Parse.Boolean(csv[4]),
                        roundLotSize: csv.GetField<int>(5),
                        testIssue: Parse.Boolean(csv[6]),
                        nasdaqSymbol: csv[7].Trim());
                });

            await Task.WhenAll(nasdaqSymbolTask, otherSymbolTask);

            return new GetAllSymbolsNasdaq.Result(
                nasdaqSymbols: nasdaqSymbolTask.Result.Item1,
                nasdaqSymbolsFileCreationTime: nasdaqSymbolTask.Result.Item2,
                otherSymbols: otherSymbolTask.Result.Item1,
                otherSymbolsFileCreationTime: otherSymbolTask.Result.Item2);
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