using System;
using System.Collections.Generic;
using System.Text;

namespace LSE.Domain.DTOs
{
    public class TradeDto
    {

        // e.g. "LSE:VOD"
        public string Ticker { get; set; }

        // average price per share
        public decimal Price { get; set; }

        // always GBP
        public string Currency { get; set; } = "GBP";

        // the moment this price value was produced
        public DateTime PriceValueAt { get; set; } = DateTime.UtcNow;
    }
}
