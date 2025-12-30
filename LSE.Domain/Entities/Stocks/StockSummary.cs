using System;
using System.Collections.Generic;
using System.Text;

namespace LSE.Domain.Entities.Stocks
{
    public class StockSummary
    {
        public string Ticker { get; set; }
        public decimal TotalPrice { get; set; }
        public long TradeCount { get; set; }
        public decimal AvgPrice { get; private set; } // computed by DB
    }
}
