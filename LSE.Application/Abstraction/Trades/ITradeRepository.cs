using LSE.Application.DTOs;
using LSE.Domain.Entities.Trades;
using System;
using System.Collections.Generic;
using System.Text;

namespace LSE.Application.Abstraction.Trades
{
    public interface ITradeRepository
    {
        Task AddAsync(Trade trade, CancellationToken token);
        Task<IEnumerable<TradeListItemDto>> GetAllTradesPagedAsync(int page, int pageSize, CancellationToken token);
    }
}
