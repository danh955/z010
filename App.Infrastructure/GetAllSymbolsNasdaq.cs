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
            /// Initializes a new instance of the <see cref="Result"/> class.
            /// </summary>
            /// <param name="nasdaqSymbols">List of NASDAQ symbols.</param>
            /// <param name="nasdaqSymbolsFileCreationTime">NASDAQ symbols file creation time.</param>
            /// <param name="otherSymbols">List of other symbols.</param>
            /// <param name="otherSymbolsFileCreationTime">Other symbols file creation time.</param>
            public Result(IEnumerable<NasdaqSymbol> nasdaqSymbols, DateTime nasdaqSymbolsFileCreationTime, IEnumerable<OtherSymbol> otherSymbols, DateTime otherSymbolsFileCreationTime)
            {
                this.NasdaqSymbols = nasdaqSymbols;
                this.NasdaqSymbolsFileCreationTime = nasdaqSymbolsFileCreationTime;
                this.OtherSymbols = otherSymbols;
                this.OtherSymbolsFileCreationTime = otherSymbolsFileCreationTime;
            }

            /// <summary>
            /// Gets list of items.
            /// </summary>
            public IEnumerable<NasdaqSymbol> NasdaqSymbols { get; private set; }

            /// <summary>
            /// Gets NASDAQ symbols file creation time.
            /// </summary>
            public DateTime NasdaqSymbolsFileCreationTime { get; private set; }

            /// <summary>
            /// Gets list of items.
            /// </summary>
            public IEnumerable<OtherSymbol> OtherSymbols { get; private set; }

            /// <summary>
            /// Gets NASDAQ symbols file creation time.
            /// </summary>
            public DateTime OtherSymbolsFileCreationTime { get; private set; }
        }
    }
}