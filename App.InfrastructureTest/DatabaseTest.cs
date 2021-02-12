// <copyright file="DatabaseTest.cs" company="None">
// Free and open source code.
// </copyright>
namespace App.InfrastructureTest
{
    using System.Linq;
    using System.Threading.Tasks;
    using App.Domain.Stocks;
    using App.Infrastructure.Database;
    using Microsoft.EntityFrameworkCore;
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
            using AppDbContext db = factory.CreateContext();

            db.Stocks.Add(new Stock() { Symbol = "test", Name = "Testing" });
            db.SaveChanges();

            var resultList = db.Stocks.ToList();
            Assert.Single(resultList);

            Stock result = resultList[0];
            Assert.Equal(1, result.Id);
            Assert.Equal("test", result.Symbol);
            Assert.Equal("Testing", result.Name);
        }
    }
}