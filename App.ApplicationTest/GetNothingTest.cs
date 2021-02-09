// <copyright file="GetNothingTest.cs" company="None">
// Free and open source code.
// </copyright>

namespace App.ApplicationTest
{
    using System.Threading;
    using System.Threading.Tasks;
    using App.Application;
    using Xunit;

    /// <summary>
    /// Get noting test class.
    /// </summary>
    public class GetNothingTest
    {
        /// <summary>
        /// Handler test.
        /// </summary>
        /// <returns>Task.</returns>
        [Fact]
        public async Task HandlerTest()
        {
            GetNothing.Query query = new GetNothing.Query() { Id = 123 };
            GetNothing.Handler handler = new GetNothing.Handler();
            GetNothing.Result result = await handler.Handle(query, CancellationToken.None);
            Assert.NotNull(result);
            Assert.IsType<string>(result.Message);
            Assert.False(string.IsNullOrWhiteSpace(result.Message));
            Assert.Contains("123", result.Message);
        }
    }
}