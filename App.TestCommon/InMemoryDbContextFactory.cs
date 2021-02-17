// <copyright file="InMemoryDbContextFactory.cs" company="None">
// Free and open source code.
// </copyright>
namespace App.TestCommon
{
    using System;
    using System.Data.Common;
    using App.Infrastructure.Database;
    using Microsoft.Data.Sqlite;
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// In memory database context factory class.
    /// From: https://www.meziantou.net/testing-ef-core-in-memory-using-sqlite.htm.
    /// </summary>
    public class InMemoryDbContextFactory : IDbContextFactory<AppEntityDbContext>, IDisposable
    {
        private DbConnection connection;

        /// <inheritdoc/>
        public AppEntityDbContext CreateDbContext()
        {
            if (this.connection == null)
            {
                this.connection = new SqliteConnection("DataSource=:memory:");
                this.connection.Open();

                using (var context = new AppEntityDbContext(this.CreateOptions()))
                {
                    context.Database.EnsureCreated();
                }
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