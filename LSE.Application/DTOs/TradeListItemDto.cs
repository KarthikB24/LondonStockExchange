using System;
using System.ComponentModel.DataAnnotations;

namespace LSE.Application.DTOs
{
    /// <summary>
    /// Represents an executed trade entry returned in paginated trade history.
    /// Returned from GET /api/v1/trades?page=1&amp;pageSize=50.
    /// </summary>
    public class TradeListItemDto
    {
        /// <summary>
        /// Stock ticker formatted as EXCHANGE:SYMBOL (e.g., LSE:VOD)
        /// </summary>
        [Required]
        public string Ticker { get; set; } = string.Empty;

        /// <summary>
        /// Full listed company name associated with the ticker
        /// </summary>
        [Required]
        public string StockName { get; set; } = string.Empty;

        /// <summary>
        /// Unique identifier of the broker who executed the trade
        /// </summary>
        [Required]
        public string BrokerId { get; set; } = string.Empty;

        /// <summary>
        /// Display name of the executing broker organization
        /// </summary>
        [Required]
        public string BrokerName { get; set; } = string.Empty;

        /// <summary>
        /// Trade currency (always GBP for LSE instruments)
        /// </summary>
        [Required]
        public string Currency { get; set; } = "GBP";

        /// <summary>
        /// BUY or SELL indicator
        /// </summary>
        [Required]
        [RegularExpression("BUY|SELL", ErrorMessage = "Side must be BUY or SELL")]
        public string Side { get; set; } = string.Empty;

        /// <summary>
        /// Executed price per share
        /// </summary>
        [Required]
        [Range(0.01, double.MaxValue)]
        public decimal Price { get; set; }

        /// <summary>
        /// Total quantity of shares traded
        /// </summary>
        [Required]
        [Range(1, int.MaxValue)]
        public decimal Quantity { get; set; }

        /// <summary>
        /// UTC timestamp indicating when the trade was executed on the exchange
        /// </summary>
        [Required]
        public DateTime ExecutedAt { get; set; }
    }
}
