// <copyright file="AppDbContext.cs" company="None">
// Free and open source code.
// </copyright>
namespace App.Infrastructure.Database
{
    using App.Domain.Stocks;
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// Application database context class.
    /// </summary>
    public class AppDbContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AppDbContext"/> class.
        /// </summary>
        /// <param name="options">DbContextOptions.</param>
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        /// <summary>
        /// Gets or sets the stock database entity.
        /// </summary>
        public DbSet<Stock> Stocks { get; set; }

        /// <inheritdoc/>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        }
    }
}