using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace LSE.Application.Commands.Trades
{
    public record AddTradeCommand(string Ticker, string BrokerCode, string Side, decimal Price, int Quantity) : IRequest;

}
