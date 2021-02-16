// <copyright file="AppDbContextSQLiteMemoryFactory.cs" company="None">
// Free and open source code.
// </copyright>
namespace App.InfrastructureTest.Database
{
    using System;
    using System.Data.Common;
    using App.Infrastructure.Database;
    using Microsoft.Data.Sqlite;
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// Application database context SQLite memory factory class.
    /// From: https://www.meziantou.net/testing-ef-core-in-memory-using-sqlite.htm.
    /// </summary>
    public class AppDbContextSQLiteMemoryFactory : IDisposable
    {
        private DbConnection connection;

        /// <summary>
        /// Create a database context where the database is held in memory.
        /// </summary>
        /// <returns>AppDbContext.</returns>
        public AppEntityDbContext CreateContext()
        {
            if (this.connection == null)
            {
                this.connection = new SqliteConnection("DataSource=:memory:");
                this.connection.Open();

                var options = this.CreateOptions();
                using var context = new AppEntityDbContext(options);
                context.Database.EnsureCreated();
            }

            return new AppEntityDbContext(this.CreateOptions());
        }

        /// <inheritdoc/>
        public void Dispose()
        {
            if (this.connection != null)
            {
                this.connection.Dispose();
                this.connection = null;
                GC.SuppressFinalize(this);
            }
        }

        private DbContextOptions<AppEntityDbContext> CreateOptions()
        {
            return new DbContextOptionsBuilder<AppEntityDbContext>()
                .UseSqlite(this.connection).Options;
        }
    }
}