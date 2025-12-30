using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace LSE.Application.Events
{
    public record TradeExecutedEvent(
        string Ticker,
        string BrokerId,
        string Side,
        decimal Price,
        decimal Quantity,
        DateTime ExecutedAt
    ) : INotification;
}
