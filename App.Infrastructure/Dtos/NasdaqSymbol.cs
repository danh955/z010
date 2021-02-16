// <copyright file="NasdaqSymbol.cs" company="None">
// Free and open source code.
// </copyright>
namespace App.Infrastructure.Dtos
{
    /// <summary>
    /// NASDAQ Symbol class.
    /// Documentation: http://www.nasdaqtrader.com/trader.aspx?id=symboldirdefs.
    /// </summary>
    public class NasdaqSymbol
    {
        /// <summary>
        /// Gets the one to four or five character identifier for each NASDAQ-listed security.
        /// </summary>
        public string Symbol { get; internal set; }

        /// <summary>
        /// Gets company issuing the security.
        /// </summary>
        public string SecurityName { get; internal set; }

        /// <summary>
        /// Gets the category assigned to the issue by NASDAQ based on Listing Requirements.
        /// Values:
        /// Q = NASDAQ Global Select MarketSM
        /// G = NASDAQ Global MarketSM
        /// S = NASDAQ Capital Market.
        /// </summary>
        public char MarketCategory { get; internal set; }

        /// <summary>
        /// Gets a value indicating whether or not the security is a test security.
        /// Values:
        /// Y = yes, it is a test issue.
        /// N = no, it is not a test issue.
        /// </summary>
        public bool TestIssue { get; internal set; }

        /// <summary>
        /// Gets indicates when an issuer has failed to submit its regulatory filings on a timely basis, has failed
        /// to meet NASDAQ's continuing listing standards, and/or has filed for bankruptcy.
        /// Values include:
        /// D = Deficient: Issuer Failed to Meet NASDAQ Continued Listing Requirements
        /// E = Delinquent: Issuer Missed Regulatory Filing Deadline
        /// Q = Bankrupt: Issuer Has Filed for Bankruptcy
        /// N = Normal(Default): Issuer Is NOT Deficient, Delinquent, or Bankrupt.
        /// G = Deficient and Bankrupt
        /// H = Deficient and Delinquent
        /// J = Delinquent and Bankrupt
        /// K = Deficient, Delinquent, and Bankrupt.
        /// </summary>
        public char FinancialStatus { get; internal set; }

        /// <summary>
        /// Gets the number of shares that make up a round lot for the given security.
        /// </summary>
        public int RoundLotSize { get; internal set; }

        /// <summary>
        /// Gets a value indicating whether the security is an exchange traded fund (ETF).
        /// Possible values:
        /// Y = Yes, security is an ETF
        /// N = No, security is not an ETF
        /// For new ETFs added to the file, the ETF field for the record will be updated to a value of "Y".
        /// </summary>
        public bool ETF { get; internal set; }

        /// <summary>
        /// Gets a value indicating whether its next shares.
        /// </summary>
        public bool NextShares { get; internal set; }

        //// File Creation Time:
        //// The last row of each Symbol Directory text file contains a time-stamp that reports the File Creation Time.
        //// The file creation time is based on when NASDAQ Trader generates the file and can be used to determine the
        //// timeliness of the associated data. The row contains the words File Creation Time followed by mmddyyyyhhmm
        //// as the first field, followed by all delimiters to round out the row.
        //// An example: File Creation Time: 1217200717:03|||||
    }
}