using LSE.Application.Abstraction.Trades;
using LSE.Application.DTOs;
using LSE.Infrastructure.Persistence;
using LSE.Domain.Entities.Trades;
using Microsoft.EntityFrameworkCore;

namespace LSE.Infrastructure.Repositories.Trades
{
    public class TradeRepository : ITradeRepository
    {
        private readonly LSEWriteDbContext _db;

        public TradeRepository(LSEWriteDbContext db)
        {
            _db = db;
        }

        public async Task AddAsync(Trade trade, CancellationToken token)
        {
            await _db.Trades.AddAsync(trade, token);
        }

        public async Task<IEnumerable<TradeListItemDto>> GetAllTradesPagedAsync(int page, int pageSize, CancellationToken token)

        {
            var skip = (page - 1) * pageSize;

            return await
                (from t in _db.Trades
                 join s in _db.Stocks on t.Ticker equals s.Ticker
                 join b in _db.Brokers on t.BrokerId equals b.BrokerCode
                 orderby t.ExecutedAt descending            // ORDER BY
                 select new TradeListItemDto
                 {
                     Ticker = t.Ticker,
                     StockName = s.CompanyName,
                     BrokerId = t.BrokerId,
                     BrokerName = b.BrokerName,
                     Side = t.Side,
                     Price = t.Price,
                     Quantity = t.Quantity,
                     Currency = "GBP",
                     ExecutedAt = t.ExecutedAt
                 })
                .Skip(skip).Take(pageSize)
                .ToListAsync(token); 

        }
    }
}
