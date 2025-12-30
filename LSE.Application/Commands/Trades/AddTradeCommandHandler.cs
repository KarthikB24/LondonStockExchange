using LSE.Application.Abstraction.Broker;
using LSE.Application.Abstraction.Stock;
using LSE.Application.Abstraction.Trades;
using LSE.Application.Events; 
using LSE.Domain.Entities.Trades;
using MediatR;

namespace LSE.Application.Commands.Trades
{
    public class AddTradeCommandHandler : IRequestHandler<AddTradeCommand>
    {
        private readonly IBrokerRepository _brokers;
        private readonly IStockRepository _stocks;
        private readonly ITradeRepository _trades;
        private readonly IStockSummaryRepository _summary;
        private readonly IUnitOfWork _uow;
        private readonly IMediator _mediator;

        public AddTradeCommandHandler(
            IBrokerRepository brokers,
            IStockRepository stocks,
            ITradeRepository trades,
            IStockSummaryRepository summary,
            IUnitOfWork uow,
             IMediator mediator)
        {
            _brokers = brokers;
            _stocks = stocks;
            _trades = trades;
            _summary = summary;
            _uow = uow;
            _mediator = mediator;
        }

        public async Task Handle(AddTradeCommand cmd, CancellationToken token)
        {
            if (!await _brokers.Exists(cmd.BrokerCode, token))
                throw new Exception($"Invalid or inactive broker: {cmd.BrokerCode}");

            if (!await _stocks.Exists(cmd.Ticker, token))
                throw new Exception($"Invalid or inactive ticker: {cmd.Ticker}");

            var trade = new Trade(cmd.Ticker, cmd.BrokerCode, cmd.Side, cmd.Price, cmd.Quantity);

            await _trades.AddAsync(trade, token);
            await _summary.UpdateAsync(cmd.Ticker, cmd.Price, token);

            await _uow.SaveChangesAsync(token);

            //For SignarlR Event Trigger
            await _mediator.Publish(new TradeExecutedEvent(
                cmd.Ticker,
                cmd.BrokerCode,
                cmd.Side,
                cmd.Price,
                cmd.Quantity,
                DateTime.UtcNow), token);
        }
    }
}
