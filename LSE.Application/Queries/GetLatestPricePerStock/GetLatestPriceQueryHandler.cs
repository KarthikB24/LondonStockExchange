
using LSE.Application.Abstraction.Stock;
using LSE.Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace LSE.Application.Queries.GetLatestPrice
{
    public class GetLatestPriceQueryHandler : IRequestHandler<GetLatestPriceQuery, TradeDto?>
    {
        private readonly IStockRepository _repo;

        public GetLatestPriceQueryHandler(IStockRepository repo)
        {
            _repo = repo;
        }

        public Task<TradeDto?> Handle(GetLatestPriceQuery req, CancellationToken token)
        {
            return _repo.GetLatestPriceAsync(req.Ticker, token);
        }
    }
}
