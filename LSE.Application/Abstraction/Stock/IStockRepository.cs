using LSE.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace LSE.Application.Abstraction.Stock
{
    public interface IStockRepository
    {
        // validation
        Task<bool> Exists(string ticker, CancellationToken token);

        // read operations
        Task<TradeDto?> GetLatestPriceAsync(string ticker, CancellationToken token);
        Task<IEnumerable<TradeDto>> GetPricesByTickersAsync(IEnumerable<string> tickers, CancellationToken token);
        Task<IEnumerable<StockListItemDto>> GetAllStocksAsync(CancellationToken token);

        // summary update (write-side aggregation)
        Task UpdateSummaryAsync(string ticker, decimal latestPrice, CancellationToken token);
    }

}
