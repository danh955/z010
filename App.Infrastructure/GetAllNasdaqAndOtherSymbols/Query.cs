// <copyright file="Query.cs" company="None">
// Free and open source code.
// </copyright>
namespace App.Infrastructure.GetAllNasdaqAndOtherSymbols
{
    using MediatR;

    /// <summary>
    /// Query parameters for the Get all NASDAQ symbols class.
    /// </summary>
    public class Query : IRequest<Result>
    {
    }
}