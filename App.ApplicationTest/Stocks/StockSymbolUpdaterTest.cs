// <copyright file="StockSymbolUpdaterTest.cs" company="None">
// Free and open source code.
// </copyright>

namespace App.ApplicationTest.Stocks
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using App.Application.Stocks.StockSymbolUpdater;
    using App.Domain.Entities;
    using App.Infrastructure;
    using App.Infrastructure.Dtos;
    using App.TestCommon;
    using MediatR;
    using Microsoft.EntityFrameworkCore;
    using Moq;
    using Xunit;

    /// <summary>
    /// Test stock symbol updater class.
    /// </summary>
    public class StockSymbolUpdaterTest
    {
        /// <summary>
        /// Handler test.
        /// </summary>
        /// <returns>Task.</returns>
        [Fact]
        public async Task StockSymbolUpdaterCommandHandlerTest()
        {
            using var contextFactory = new InMemoryDbContextFactory();
            var mockResult = GetMockResult();

            using (var db = contextFactory.CreateDbContext())
            {
                // Load the database with one stock.
                var stock = mockResult.NasdaqSymbols.First();
                db.Stocks.Add(new StockEntity() { Symbol = stock.Symbol, Name = stock.SecurityName });
                await db.SaveChangesAsync();
            }

            var mediatorMock = new Mock<IMediator>();
            mediatorMock.Setup(x => x.Send(It.IsAny<GetAllSymbolsNasdaq.Query>(), It.IsAny<CancellationToken>())).ReturnsAsync(mockResult);

            var command = new StockSymbolUpdater.Command();
            var handler = new StockSymbolUpdater.Handler(mediatorMock.Object, contextFactory);
            await handler.Handle(command, CancellationToken.None);

            List<StockEntity> dbStocks;
            using (var db = contextFactory.CreateDbContext())
            {
                dbStocks = await db.Stocks.ToListAsync();
            }

            Assert.Equal(6, dbStocks.Count);
        }

        private static GetAllSymbolsNasdaq.Result GetMockResult()
        {
            var nasdaqSymbols = new List<NasdaqSymbol>()
            {
                new NasdaqSymbol("AACG", "ATA Creativity Global", 'G', false, 'N', 100, false, false),
                new NasdaqSymbol("CASY", "Casey's General Stores, Inc.", 'Q', false, 'N', 100, false, false),
                new NasdaqSymbol("ESPO", "VanEck Vectors Video Gaming and eSports ETF", 'G', false, 'N', 100, true, false),
            };

            var otherSymbol = new List<OtherSymbol>()
            {
                new OtherSymbol("A", "Agilest Technologies", 'N', "A", false, 100, false, "A"),
                new OtherSymbol("ECL", "Ecolab Inc. Common Stock", 'N', "ECL", false, 100, false, "ECL"),
                new OtherSymbol("HE", "Hawaiian Electric Industries, Inc. Common Stock", 'N', "HE", false, 100, false, "HE"),
            };

            return new GetAllSymbolsNasdaq.Result(nasdaqSymbols, new DateTime(2021, 1, 21), otherSymbol, new DateTime(2021, 1, 21));
        }
    }
}