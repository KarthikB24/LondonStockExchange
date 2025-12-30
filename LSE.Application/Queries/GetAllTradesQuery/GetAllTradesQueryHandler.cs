using LSE.Application.DTOs;
using MediatR;
using LSE.Application.Abstraction.Trades; 

namespace LSE.Application.Queries.GetListOfTrades
{
    public class GetAllTradesQueryHandler
        : IRequestHandler<GetAllTradesQuery, IEnumerable<TradeListItemDto>>
    {
        private readonly ITradeRepository _tradeRepo;

        public GetAllTradesQueryHandler(ITradeRepository tradeRepo)
        {
            _tradeRepo = tradeRepo;
        }

        public async Task<IEnumerable<TradeListItemDto>> Handle(
            GetAllTradesQuery request,
            CancellationToken cancellationToken)
        {
            return await _tradeRepo.GetAllTradesPagedAsync(
                request.Page,
                request.PageSize,
                cancellationToken);
        }
    }
}
