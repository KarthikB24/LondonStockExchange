using System;
using System.Collections.Generic;
using System.Text;

namespace LSE.Domain.DTOs
{
    public class TradeListItemDto
    {
        public string Ticker { get; set; }
        public string StockName { get; set; }   // new
        public string BrokerId { get; set; }
        public string BrokerName { get; set; }  // new
        public string Currency { get; set; } = "GBP";
        public string Side { get; set; }
        public decimal Price { get; set; }
        public decimal Quantity { get; set; }
        public DateTime ExecutedAt { get; set; }
    } 
}
