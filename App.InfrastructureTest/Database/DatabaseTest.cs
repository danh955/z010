// <copyright file="DatabaseTest.cs" company="None">
// Free and open source code.
// </copyright>
namespace App.InfrastructureTest.Database
{
    using System.Linq;
    using App.Domain.Entities;
    using App.Infrastructure.Database;
    using Xunit;

    /// <summary>
    /// Database test class.
    /// </summary>
    public class DatabaseTest
    {
        /// <summary>
        /// Stock add test.
        /// </summary>
        [Fact]
        public void StockAddTest()
        {
            using var factory = new AppDbContextSQLiteMemoryFactory();
            using AppEntityDbContext db = factory.CreateContext();

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