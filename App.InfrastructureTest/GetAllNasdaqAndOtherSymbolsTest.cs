// <copyright file="GetAllNasdaqAndOtherSymbolsTest.cs" company="None">
// Free and open source code.
// </copyright>

namespace App.InfrastructureTest
{
    using System.Linq;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using App.Infrastructure.GetAllNasdaqAndOtherSymbols;
    using Moq;
    using Xunit;

    /// <summary>
    /// Test get all NASDAQ and other symbols class.
    /// </summary>
    public class GetAllNasdaqAndOtherSymbolsTest
    {
        /// <summary>
        /// Handler test.
        /// </summary>
        /// <returns>Task.</returns>
        [Fact]
        public async Task HandlerTest()
        {
            HttpClient httpClient = new HttpClient();
            var mock = new Mock<IHttpClientFactory>();
            mock.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(httpClient);

            Handler handler = new Handler(mock.Object);
            Result result = await handler.Handle(new Query(), CancellationToken.None);
            Assert.NotNull(result);
            Assert.NotNull(result.NasdaqSymbols);
            Assert.NotNull(result.OtherSymbols);
            Assert.True(result.NasdaqSymbols.Any());
            Assert.True(result.OtherSymbols.Any());
        }
    }
}