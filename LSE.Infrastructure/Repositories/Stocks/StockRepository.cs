using Dapper;
using LSE.Application.Abstraction.Stock;
using LSE.Application.DTOs;
using LSE.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore; 
using System.Data; 

namespace LSE.Infrastructure.Repositories.Stocks
{
    public class StockRepository : IStockRepository
    {
        private readonly IDbConnection _db;          // Dapper read
        private readonly LSEWriteDbContext _writeDb;    // EF write

        public StockRepository(IDbConnection db, LSEWriteDbContext writeDb)
        {
            _db = db;
            _writeDb = writeDb;
        }

        public Task<bool> Exists(string ticker, CancellationToken token)
        {
            return _writeDb.Stocks.AnyAsync(x => x.Ticker == ticker && x.IsActive, token);
        }

        public async Task<TradeDto?> GetLatestPriceAsync(string ticker, CancellationToken token)
        {
            const string sql = @"
                SELECT 
	                Ticker,
	                AvgPrice AS Price,
	                TradeCount AS Quantity,
	                GETUTCDATE() AS CreatedAT
                FROM StockSummary
                WHERE Ticker = @ticker;
            ";

            return await _db.QueryFirstOrDefaultAsync<TradeDto>(
                sql, new { ticker });
        }

        public async Task<IEnumerable<TradeDto>> GetPricesByTickersAsync(
            IEnumerable<string> tickers, CancellationToken token)
        {
            var results = new List<TradeDto>();

            foreach (var ticker in tickers.Distinct())
            {
                var dto = await GetLatestPriceAsync(ticker, token);
                if (dto != null)
                    results.Add(dto);
            }

            return results;
        }

        public async Task<IEnumerable<StockListItemDto>> GetAllStocksAsync(CancellationToken token)
        {
            const string sql = @"
                SELECT 
                    Ticker,
                    CompanyName,
                    IsActive
                FROM Stocks
                WHERE IsActive = 1;
            ";

            return await _db.QueryAsync<StockListItemDto>(sql);
        }

        public async Task UpdateSummaryAsync(string ticker, decimal latestPrice, CancellationToken token)
        {
            var summary = await _writeDb.StockSummary.FindAsync(new object[] { ticker }, token);

            if (summary is null)
            {
                _writeDb.StockSummary.Add(new()
                {
                    Ticker = ticker,
                    TotalPrice = latestPrice,
                    TradeCount = 1
                });
            }
            else
            {
                summary.TotalPrice += latestPrice;
                summary.TradeCount++;
            }
        }
 
    }
}
