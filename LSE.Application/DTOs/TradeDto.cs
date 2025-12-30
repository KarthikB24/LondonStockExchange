using System;
using System.ComponentModel.DataAnnotations;

namespace LSE.Application.DTOs
{
    /// <summary>
    /// Represents the latest evaluated price information for a specific stock.
    /// Returned from GET /api/v1/stocks/{ticker} and /api/v1/stocks/prices.
    /// </summary>
    public class TradeDto
    {
        /// <summary>
        /// Ticker formatted as EXCHANGE:SYMBOL (e.g., LSE:VOD)
        /// </summary>
        [Required]
        public string Ticker { get; set; } = string.Empty;

        /// <summary>
        /// Latest evaluated or average traded price per share
        /// </summary>
        [Required]
        public decimal Price { get; set; }

        /// <summary>
        /// Currency in which the price is quoted (always GBP for LSE instruments)
        /// </summary>
        [Required]
        public string Currency { get; set; } = "GBP";

        /// <summary>
        /// UTC timestamp indicating when this price value was last calculated
        /// </summary>
        [Required]
        public DateTime PriceValueAt { get; set; } = DateTime.UtcNow;
    }
}
