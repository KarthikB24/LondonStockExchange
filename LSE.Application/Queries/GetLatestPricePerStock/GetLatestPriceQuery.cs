using LSE.Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace LSE.Application.Queries.GetLatestPrice
{
    public record GetLatestPriceQuery(string Ticker) : IRequest<TradeDto?>;

    //public class GetLatestPriceQuery : IRequest<TradeDto?>
    //{
    //    public string Ticker { get; set; } = string.Empty;
    //}
}
