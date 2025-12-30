using System;
using System.Collections.Generic;
using System.Text;

namespace LSE.Application.Abstraction.Stock
{
    public interface IStockSummaryRepository
    {
        Task UpdateAsync(string ticker, decimal latestPrice, CancellationToken token);
        Task<decimal?> GetAvgPriceAsync(string ticker, CancellationToken token);
    } 
}
