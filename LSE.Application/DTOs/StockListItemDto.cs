using System.ComponentModel.DataAnnotations;

namespace LSE.Application.DTOs
{
    /// <summary>
    /// Represents a publicly tradable stock listing available on the exchange.
    /// Returned from GET /api/v1/stocks.
    /// </summary>
    public class StockListItemDto
    {
        /// <summary>
        /// Stock ticker in format EXCHANGE:SYMBOL (e.g., LSE:VOD)
        /// </summary>
        [Required]
        public string Ticker { get; set; } = string.Empty;

        /// <summary>
        /// Full legal name of the company associated with the stock ticker
        /// </summary>
        [Required]
        public string CompanyName { get; set; } = string.Empty;
    }
}
