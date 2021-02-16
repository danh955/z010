﻿// <copyright file="OtherSymbol.cs" company="None">
// Free and open source code.
// </copyright>

namespace App.Infrastructure.Dtos
{
    /// <summary>
    /// Other symbols (not NASDAQ) class.
    /// Documentation: http://www.nasdaqtrader.com/trader.aspx?id=symboldirdefs.
    /// </summary>
    public class OtherSymbol
    {
        /// <summary>
        /// Gets identifier for each security used in ACT and CTCI connectivity protocol. Typical identifiers
        /// have 1-5 character root symbol and then 1-3 characters for suffixes. Allow up to 14 characters.
        /// </summary>
        public string ActSymbol { get; internal set; }

        /// <summary>
        /// Gets the name of the security including additional information, if applicable. Examples are security
        /// type (common stock, preferred stock, etc.) or class (class A or B, etc.). Allow up to 255 characters.
        /// </summary>
        public string SecurityName { get; internal set; }

        /// <summary>
        /// Gets the listing stock exchange or market of a security.
        /// Allowed values are:
        /// A = NYSE MKT
        /// N = New York Stock Exchange(NYSE)
        /// P = NYSE ARCA
        /// Z = BATS Global Markets(BATS)
        /// V = Investors' Exchange, LLC (IEXG).
        /// </summary>
        public char Exchange { get; internal set; }

        /// <summary>
        /// Gets identifier of the security used to disseminate data via the SIAC Consolidated Quotation System (CQS) and
        /// Consolidated Tape System (CTS) data feeds. Typical identifiers have 1-5 character root symbol and then
        /// 1-3 characters for suffixes. Allow up to 14 characters.
        /// </summary>
        public string CqsSymbol { get; internal set; }

        /// <summary>
        /// Gets a value indicating whether the security is an exchange traded fund (ETF). Possible values:
        /// Y = Yes, security is an ETF
        /// N = No, security is not an ETF
        /// For new ETFs added to the file, the ETF field for the record will be updated to a value of "Y".
        /// </summary>
        public bool ETF { get; internal set; }

        /// <summary>
        /// Gets indicates the number of shares that make up a round lot for the given security. Allow up to 6 digits.
        /// </summary>
        public int RoundLotSize { get; internal set; }

        /// <summary>
        /// Gets a value indicating whether the security is a test security.
        /// Y = Yes, it is a test issue.
        /// N = No, it is not a test issue.
        /// </summary>
        public bool TestIssue { get; internal set; }

        /// <summary>
        /// Gets identifier of the security used to in various NASDAQ connectivity protocols and NASDAQ market data feeds. Typical
        /// identifiers have 1-5 character root symbol and then 1-3 characters for suffixes. Allow up to 14 characters.
        /// </summary>
        public string NASDAQSymbol { get; internal set; }

        //// File Creation Time:
        //// The last row of each Symbol Directory text file contains a time-stamp that reports the File Creation Time.
        //// The file creation time is based on when NASDAQ Trader generates the file and can be used to determine the
        //// timeliness of the associated data. The row contains the words File Creation Time followed by mmddyyyyhhmm
        //// as the first field, followed by all delimiters to round out the row.
        //// An example: File Creation Time: File Creation Time: 1217200717:03||||||
    }
}