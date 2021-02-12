// <copyright file="AppDbContextSQLiteMemoryFactory.cs" company="None">
// Free and open source code.
// </copyright>
namespace App.InfrastructureTest
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
        public AppDbContext CreateContext()
        {
            if (this.connection == null)
            {
                this.connection = new SqliteConnection("DataSource=:memory:");
                this.connection.Open();

                var options = this.CreateOptions();
                using var context = new AppDbContext(options);
                context.Database.EnsureCreated();
            }

            return new AppDbContext(this.CreateOptions());
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

        private DbContextOptions<AppDbContext> CreateOptions()
        {
            return new DbContextOptionsBuilder<AppDbContext>()
                .UseSqlite(this.connection).Options;
        }
    }
}