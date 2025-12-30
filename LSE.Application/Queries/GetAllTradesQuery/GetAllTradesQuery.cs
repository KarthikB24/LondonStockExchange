using LSE.Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace LSE.Application.Queries.GetListOfTrades
{
    public record GetAllTradesQuery(int Page = 1, int PageSize = 100) : IRequest<IEnumerable<TradeListItemDto>>;


}
