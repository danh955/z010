// <copyright file="AppEntityDbContextText.cs" company="None">
// Free and open source code.
// </copyright>
namespace App.InfrastructureTest.Database
{
    using System.Linq;
    using App.Domain.Entities;
    using App.Infrastructure.Database;
    using App.TestCommon;
    using Xunit;

    /// <summary>
    /// Database test class.
    /// </summary>
    public class AppEntityDbContextText
    {
        /// <summary>
        /// Stock add test.
        /// </summary>
        [Fact]
        public void StockAddTest()
        {
            using var contextFactory = new InMemoryDbContextFactory();
            using AppEntityDbContext db = contextFactory.CreateDbContext();

            db.Stocks.Add(new StockEntity() { Symbol = "test", Name = "Testing" });
            db.SaveChanges();

            var resultList = db.Stocks.ToList();
            Assert.Single(resultList);

            StockEntity result = resultList[0];
            Assert.Equal(1, result.Id);
            Assert.Equal("test", result.Symbol);
            Assert.Equal("Testing", result.Name);
        }
    }
}