﻿// <copyright file="ServiceCollectionExtensions.cs" company="None">
// Free and open source code.
// </copyright>

namespace App.Infrastructure
{
    using App.Infrastructure.Database;
    using MediatR;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;

    /// <summary>
    /// Service collection extensions class.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Add core project to the service collection.
        /// </summary>
        /// <param name="service">IServiceCollection.</param>
        /// <param name="sqliteConnecttionString">SQLite connection string.</param>
        /// <returns>Updated IServiceCollection.</returns>
        public static IServiceCollection AddAppInfrastructure(this IServiceCollection service, string sqliteConnecttionString)
        {
            service.AddPooledDbContextFactory<AppEntityDbContext>(options => options.UseSqlite(sqliteConnecttionString));
            service.AddMediatR(typeof(ServiceCollectionExtensions));
            return service;
        }
    }
}