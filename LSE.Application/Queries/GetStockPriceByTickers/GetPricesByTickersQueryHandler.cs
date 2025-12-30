using LSE.Application.Abstraction.Stock; 
using LSE.Application.DTOs;
using MediatR; 

namespace LSE.Application.Queries.GetStockPriceByTickers
{
    public class GetPricesByTickersQueryHandler
        : IRequestHandler<GetPricesByTickersQuery, List<TradeDto>>
    {
        private readonly IStockRepository _repo;

        public GetPricesByTickersQueryHandler(IStockRepository repo)
        {
            _repo = repo;
        }

        public async Task<List<TradeDto>> Handle(GetPricesByTickersQuery req, CancellationToken token)
        {
            var results = new List<TradeDto>();

            foreach (var ticker in req.Tickers.Distinct())
            {
                var price = await _repo.GetLatestPriceAsync(ticker, token);
                if (price != null)
                    results.Add(price);
            }

            return results;
        }
    }
}
