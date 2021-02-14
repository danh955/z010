// <copyright file="StockSymbolUpdaterCommandHandler.cs" company="None">
// Free and open source code.
// </copyright>
namespace App.Application.Stocks.StockSymbolUpdater
{
    using System.Threading;
    using System.Threading.Tasks;
    using MediatR;

    /// <summary>
    /// Command handler for updating the database symbols.
    /// </summary>
    public class StockSymbolUpdaterCommandHandler : IRequestHandler<StockSymbolUpdaterCommand>
    {
        /// <inheritdoc/>
        public async Task<Unit> Handle(StockSymbolUpdaterCommand request, CancellationToken cancellationToken)
        {
            await Task.Delay(10, cancellationToken);
            return Unit.Value;
        }
    }
}