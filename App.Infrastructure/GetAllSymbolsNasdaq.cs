// <copyright file="GetAllSymbolsNasdaq.cs" company="None">
// Free and open source code.
// </copyright>
namespace App.Infrastructure
{
    using System;
    using System.Collections.Generic;
    using App.Infrastructure.Dtos;
    using MediatR;

    /// <summary>
    /// Get all symbols from NASDAQ website.
    /// </summary>
    public class GetAllSymbolsNasdaq
    {
        /// <summary>
        /// Get nothing query.
        /// </summary>
        public class Query : IRequest<Result>
        {
        }

        /// <summary>
        /// Result of getting all the NASDAQ systems class.
        /// </summary>
        public class Result
        {
            /// <summary>
            /// Gets list of items.
            /// </summary>
            public IEnumerable<NasdaqSymbol> NasdaqSymbols { get; internal set; }

            /// <summary>
            /// Gets NASDAQ symbols file creation time.
            /// </summary>
            public DateTime NasdaqSymbolsFileCreationTime { get; internal set; }

            /// <summary>
            /// Gets list of items.
            /// </summary>
            public IEnumerable<OtherSymbol> OtherSymbols { get; internal set; }

            /// <summary>
            /// Gets NASDAQ symbols file creation time.
            /// </summary>
            public DateTime OtherSymbolsFileCreationTime { get; internal set; }
        }
    }
}