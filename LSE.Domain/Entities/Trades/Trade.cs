using System;
using System.Collections.Generic;
using System.Text;

namespace LSE.Domain.Entities.Trades
{
    public class Trade
    {
        public Guid Id { get; private set; } = Guid.NewGuid();
        public string Ticker { get; private set; }
        public string BrokerId { get; private set; }
        public string Side { get; private set; }  // Buy or Sell
        public decimal Price { get; private set; }
        public decimal Quantity { get; private set; }
        public DateTime ExecutedAt { get; private set; } = DateTime.UtcNow;

        private Trade() { } // EF Core

        public Trade(string ticker, string brokerId, string side, decimal price, decimal qty)
        {
            Ticker = ticker;
            BrokerId = brokerId;
            Side = side;
            Price = price;
            Quantity = qty;
        }
    }
}
