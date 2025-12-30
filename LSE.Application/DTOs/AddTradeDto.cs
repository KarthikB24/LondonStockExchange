using System.ComponentModel.DataAnnotations;

namespace LSE.Application.DTOs
{
    /// <summary>
    /// Trade request used to execute BUY/SELL orders for a listed stock.
    /// Example:
    /// {
    ///   "ticker": "LSE:VOD",
    ///   "brokerId": "BRK-JPM",
    ///   "price": 252.7500,
    ///   "quantity": 100
    /// }
    /// </summary>
    public class AddTradeDto
    {
        /// <summary>
        /// Stock ticker formatted as EXCHANGE:SYMBOL (e.g., LSE:VOD, LSE:SHEL)
        /// </summary>
        [Required]
        [RegularExpression(@"^[A-Z]{3}:[A-Z0-9]+$",
            ErrorMessage = "Ticker must be in format LSE:VOD")]
        public string Ticker { get; set; } = string.Empty;

        /// <summary>
        /// Unique broker code for the submitting broker (from authentication response)
        /// </summary>
        [Required]
        public string BrokerId { get; set; } = string.Empty;

        /// <summary>
        /// Executed price of the trade in instrument currency
        /// </summary>
        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0")]
        public decimal Price { get; set; }

        /// <summary>
        /// Number of shares traded (minimum 1)
        /// </summary>
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1")]
        public int Quantity { get; set; }
    }
}
