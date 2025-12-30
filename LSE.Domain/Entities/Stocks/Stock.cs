using System;
using System.Collections.Generic;
using System.Text;

namespace LSE.Domain.Entities.Stock
{
    public class Stock
    {
        public Guid Id { get; private set; } = Guid.NewGuid();
        public string Ticker { get; private set; }
        public string CompanyName { get; private set; }
        public bool IsActive { get; private set; } = true;

        private Stock() { }

        public Stock(string ticker, string name)
        {
            Ticker = ticker;
            CompanyName = name;
        }
    }
}
