// <copyright file="StockSymbolUpdaterCommand.cs" company="None">
// Free and open source code.
// </copyright>
namespace App.Application.Stocks.StockSymbolUpdater
{
    using MediatR;

    /// <summary>
    /// Update the database stock table from the NASDAQ symbols file.
    /// </summary>
    public class StockSymbolUpdaterCommand : IRequest
    {
    }
}