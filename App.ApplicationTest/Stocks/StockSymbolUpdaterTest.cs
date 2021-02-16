// <copyright file="StockSymbolUpdaterTest.cs" company="None">
// Free and open source code.
// </copyright>

namespace App.ApplicationTest.Stocks
{
    using System.Threading;
    using System.Threading.Tasks;
    using App.Application.Stocks.StockSymbolUpdater;
    using App.Infrastructure;
    using MediatR;
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
            var mediatorMock = new Mock<IMediator>();
            mediatorMock.Setup(x => x.Send(It.IsAny<GetAllSymbolsNasdaq.Query>(), It.IsAny<CancellationToken>())).ReturnsAsync(new GetAllSymbolsNasdaq.Result());

            var command = new StockSymbolUpdater.Command();
            var handler = new StockSymbolUpdater.Handler(mediatorMock.Object);
            await handler.Handle(command, CancellationToken.None);
        }
    }
}