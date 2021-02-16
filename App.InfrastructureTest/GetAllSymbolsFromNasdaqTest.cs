// <copyright file="GetAllSymbolsFromNasdaqTest.cs" company="None">
// Free and open source code.
// </copyright>
namespace App.InfrastructureTest
{
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using App.Infrastructure;
    using App.Infrastructure.Handlers;
    using App.InfrastructureTest.Helper;
    using Xunit;

    /// <summary>
    /// Test get all NASDAQ and other symbols class.
    /// </summary>
    public class GetAllSymbolsFromNasdaqTest
    {
        /// <summary>
        /// Handler test.
        /// </summary>
        /// <returns>Task.</returns>
        [Fact]
        public async Task GetAllSymbolsFromNasdaqQueryHandlerTest()
        {
            var handler = new GetAllSymbolsNasdaqHandler(MockData.MockIHttpClientFactoryForNasdaqSymbols());
            var result = await handler.Handle(new GetAllSymbolsNasdaq.Query(), CancellationToken.None);

            Assert.NotNull(result);
            Assert.NotNull(result.NasdaqSymbols);
            Assert.NotNull(result.OtherSymbols);
            Assert.True(result.NasdaqSymbols.Any());
            Assert.True(result.OtherSymbols.Any());
        }
    }
}