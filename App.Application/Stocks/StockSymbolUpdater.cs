// <copyright file="StockSymbolUpdater.cs" company="None">
// Free and open source code.
// </copyright>
namespace App.Application.Stocks.StockSymbolUpdater
{
    using System.Threading;
    using System.Threading.Tasks;
    using App.Infrastructure;
    using MediatR;

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
            private readonly IMediator mediator;

            /// <summary>
            /// Initializes a new instance of the <see cref="Handler"/> class.
            /// </summary>
            /// <param name="mediator">IMediator.</param>
            public Handler(IMediator mediator)
            {
                this.mediator = mediator;
            }

            /// <inheritdoc/>
            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                var result = await this.mediator.Send(new GetAllSymbolsNasdaq.Query(), cancellationToken);

                await Task.Delay(10, cancellationToken);
                return Unit.Value;
            }
        }
    }
}