using LSE.Application.Abstraction.Stock;
using LSE.Domain.Entities.Stocks;
using LSE.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace LSE.Infrastructure.Repositories.Stocks
{
    public class StockSummaryRepository : IStockSummaryRepository
    {
        private readonly LSEWriteDbContext _db;

        public StockSummaryRepository(LSEWriteDbContext db)
        {
            _db = db;
        }

        public async Task UpdateAsync(string ticker, decimal latestPrice, CancellationToken token)
        {
            var summary = await _db.StockSummary
                .FirstOrDefaultAsync(x => x.Ticker == ticker, token);

            if (summary is null)
            {
                summary = new StockSummary
                {
                    Ticker = ticker,
                    TotalPrice = latestPrice,
                    TradeCount = 1
                };
                _db.StockSummary.Add(summary);
            }
            else
            {
                summary.TotalPrice += latestPrice;
                summary.TradeCount++;
            }
        }

        public async Task<decimal?> GetAvgPriceAsync(string ticker, CancellationToken token)
        {
            var summary = await _db.StockSummary
                .FirstOrDefaultAsync(x => x.Ticker == ticker, token);

            return summary?.AvgPrice;
        }
    }
}
