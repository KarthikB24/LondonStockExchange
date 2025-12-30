using System;
using System.Collections.Generic;
using System.Text;

namespace LSE.Domain.DTOs
{
    public class AddTradeDto
    {
        public string Ticker { get; set; } = string.Empty;
        public string BrokerId { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public decimal Quantity { get; set; }
    }
}
