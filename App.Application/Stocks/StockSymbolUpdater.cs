// <copyright file="StockSymbolUpdater.cs" company="None">
// Free and open source code.
// </copyright>
namespace App.Application.Stocks.StockSymbolUpdater
{
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using App.Domain.Entities;
    using App.Infrastructure;
    using App.Infrastructure.Database;
    using MediatR;
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// Stock symbol updater class.
    /// </summary>
    public static class StockSymbolUpdater
    {
        /// <summary>
        /// Update the database stock table from the NASDAQ symbols file.
        /// </summary>
        public class Command : IRequest
        {
        }

        /// <summary>
        /// Command handler for updating the database symbols.
        /// </summary>
        public class Handler : IRequestHandler<Command>
        {
            private readonly IDbContextFactory<AppEntityDbContext> contextFactory;
            private readonly IMediator mediator;

            /// <summary>
            /// Initializes a new instance of the <see cref="Handler"/> class.
            /// </summary>
            /// <param name="mediator">IMediator.</param>
            /// <param name="contextFactory">IDbContextFactory for AppEntityDbContext.</param>
            public Handler(IMediator mediator, IDbContextFactory<AppEntityDbContext> contextFactory)
            {
                this.mediator = mediator;
                this.contextFactory = contextFactory;
            }

            /// <inheritdoc/>
            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                using var db = this.contextFactory.CreateDbContext();

                var allSymbolsTask = this.mediator.Send(new GetAllSymbolsNasdaq.Query(), cancellationToken);
                var stocksTask = db.Stocks.AsNoTracking().Select(s => s.Symbol).ToListAsync(cancellationToken);
                await Task.WhenAll(allSymbolsTask, stocksTask);

                var dbSymbols = stocksTask.Result.ToHashSet();
                var allSymbols = allSymbolsTask.Result.NasdaqSymbols
                        .Where(s => s.Symbol.All(char.IsLetter) && !s.TestIssue)
                        .Select(s => new { Symbol = s.Symbol.ToUpper(), Name = s.SecurityName })
                    .Concat(allSymbolsTask.Result.OtherSymbols
                        .Where(s => s.NASDAQSymbol.All(char.IsLetter) && !s.TestIssue)
                        .Select(s => new { Symbol = s.NASDAQSymbol.ToUpper(), Name = s.SecurityName }))
                    .ToList();

                var newSymbols = allSymbols
                    .Where(s => !dbSymbols.Contains(s.Symbol))
                    .Select(s => new StockEntity() { Symbol = s.Symbol, Name = s.Name })
                    .ToList();

                if (newSymbols.Any())
                {
                    db.Stocks.AddRange(newSymbols);
                    await db.SaveChangesAsync(cancellationToken);
                }

                return Unit.Value;
            }
        }
    }
}