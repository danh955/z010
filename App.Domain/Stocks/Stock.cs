// <copyright file="Stock.cs" company="None">
// Free and open source code.
// </copyright>
namespace App.Domain.Stocks
{
    /// <summary>
    /// Stock class.
    /// </summary>
    public class Stock
    {
        /// <summary>
        /// Gets or sets the stock ID.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        ///  Gets or sets the stock symbol.
        /// </summary>
        public string Symbol { get; set; }

        /// <summary>
        /// Gets or sets the stock name.
        /// </summary>
        public string Name { get; set; }
    }
}