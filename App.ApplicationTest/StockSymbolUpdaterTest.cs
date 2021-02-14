// <copyright file="StockSymbolUpdaterTest.cs" company="None">
// Free and open source code.
// </copyright>

namespace App.ApplicationTest
{
    using System.Threading;
    using System.Threading.Tasks;
    using App.Application.Stocks.StockSymbolUpdater;
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
        public async Task HandlerTest()
        {
            //// TODO: Naming is bad.
            var command = new StockSymbolUpdaterCommand();
            var handler = new StockSymbolUpdaterCommandHandler();
            await handler.Handle(command, CancellationToken.None);
        }
    }
}