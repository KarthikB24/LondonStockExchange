using LSE.Application.Events;
using MediatR;
using Microsoft.AspNetCore.SignalR;

namespace LSE.StocksAPI.Realtime
{
    public class TradeExecutedEventHandler : INotificationHandler<TradeExecutedEvent>
    {
        private readonly IHubContext<TradesHub> _hub;

        public TradeExecutedEventHandler(IHubContext<TradesHub> hub)
        {
            _hub = hub;
        }

        public async Task Handle(TradeExecutedEvent evt, CancellationToken cancellationToken)
        {
            await _hub.Clients.All.SendAsync("TradeExecuted", evt, cancellationToken);
        }
    }
}
