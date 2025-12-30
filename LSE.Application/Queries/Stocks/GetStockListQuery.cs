using LSE.Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace LSE.Application.Queries.Stocks
{
    public record GetStockListQuery : IRequest<IEnumerable<StockListItemDto>>;
}
