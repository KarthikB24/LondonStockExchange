using LSE.Application.Abstraction.Stock; 
using LSE.Application.DTOs;
using MediatR; 

namespace LSE.Application.Queries.Stocks
{
    public class GetStockListQueryHandler
       : IRequestHandler<GetStockListQuery, IEnumerable<StockListItemDto>>
    {
        private readonly IStockRepository _stocks;

        public GetStockListQueryHandler(IStockRepository stocks)
        {
            _stocks = stocks;
        }

        public async Task<IEnumerable<StockListItemDto>> Handle(GetStockListQuery request, CancellationToken token)
        {
            return await _stocks.GetAllStocksAsync(token);
        }
    }
}
