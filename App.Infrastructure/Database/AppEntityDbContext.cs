// <copyright file="AppEntityDbContext.cs" company="None">
// Free and open source code.
// </copyright>
namespace App.Infrastructure.Database
{
    using App.Domain.Entities;
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// Application database context class.
    /// </summary>
    public class AppEntityDbContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AppEntityDbContext"/> class.
        /// </summary>
        /// <param name="options">DbContextOptions.</param>
        public AppEntityDbContext(DbContextOptions<AppEntityDbContext> options)
            : base(options)
        {
        }

        /// <summary>
        /// Gets or sets the stock database entity.
        /// </summary>
        public DbSet<StockEntity> Stocks { get; set; }

        /// <inheritdoc/>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppEntityDbContext).Assembly);
        }
    }
}