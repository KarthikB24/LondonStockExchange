using LSE.Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace LSE.Application.Queries.GetStockPriceByTickers
{ 
    public record GetPricesByTickersQuery(List<string> Tickers)
        : IRequest<List<TradeDto>>;
}
