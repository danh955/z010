// <copyright file="Result.cs" company="None">
// Free and open source code.
// </copyright>
namespace App.Infrastructure.GetAllNasdaqAndOtherSymbols
{
    using System;
    using System.Collections.Generic;

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