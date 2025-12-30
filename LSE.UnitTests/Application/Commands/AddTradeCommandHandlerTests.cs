using FluentAssertions;
using LSE.Application.Abstraction.Broker;
using LSE.Application.Abstraction.Stock;
using LSE.Application.Abstraction.Trades;
using LSE.Application.Commands;
using LSE.Application.Commands.Trades;
using LSE.Application.Abstraction;
using LSE.Domain.Entities.Trades;
using Moq;
using Xunit;
using MediatR;

namespace LSE.UnitTests.Application.Commands
{
    public class AddTradeCommandHandlerTests
    {
        [Fact]
        public async Task Handle_ShouldSaveTrade_WhenValid()
        {
            // Arrange
            var brokers = new Mock<IBrokerRepository>();
            var stocks = new Mock<IStockRepository>();
            var summary = new Mock<IStockSummaryRepository>();
            var trades = new Mock<ITradeRepository>();
            var uow = new Mock<IUnitOfWork>();
            var mediator = new Mock<IMediator>();

            brokers.Setup(r => r.Exists("B1", It.IsAny<CancellationToken>())).ReturnsAsync(true);
            stocks.Setup(r => r.Exists("LSE:HSBA", It.IsAny<CancellationToken>())).ReturnsAsync(true);

            {
                var handler = new AddTradeCommandHandler(brokers.Object, stocks.Object, trades.Object, summary.Object, uow.Object, mediator.Object);

                var cmd = new AddTradeCommand("LSE:HSBA", "B1", "Buy", 100, 1);

                // Act
                await handler.Handle(cmd, CancellationToken.None);

                // Assert
                trades.Verify(r => r.AddAsync(It.IsAny<Trade>(), It.IsAny<CancellationToken>()), Times.Once);
                stocks.Verify(r => r.UpdateSummaryAsync("LSE:HSBA", 100, It.IsAny<CancellationToken>()), Times.Once);
                uow.Verify(r => r.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
            }
        }

        [Fact]
        public async Task Handle_ShouldThrow_WhenBrokerInvalid()
        {
            var brokers = new Mock<IBrokerRepository>();
            var stocks = new Mock<IStockRepository>();
            var trades = new Mock<ITradeRepository>();
            var summary = new Mock<IStockSummaryRepository>();
            var uow = new Mock<IUnitOfWork>();
            var mediator = new Mock<IMediator>();

            brokers.Setup(r => r.Exists("X1", It.IsAny<CancellationToken>())).ReturnsAsync(false);

            var handler = new AddTradeCommandHandler(brokers.Object, stocks.Object, trades.Object, summary.Object, uow.Object, mediator.Object);

            var cmd = new AddTradeCommand("LSE:VOD", "X1", "Buy", 100, 1);

            Func<Task> act = () => handler.Handle(cmd, CancellationToken.None);

            await act.Should().ThrowAsync<Exception>()
                .WithMessage("Invalid or inactive broker: X1");
        }

        [Fact]
        public async Task Handle_ShouldThrow_WhenStockInvalid()
        {
            var brokers = new Mock<IBrokerRepository>();
            var stocks = new Mock<IStockRepository>();
            var trades = new Mock<ITradeRepository>();
            var summary = new Mock<IStockSummaryRepository>();
            var uow = new Mock<IUnitOfWork>();
            var mediator = new Mock<IMediator>();

            brokers.Setup(r => r.Exists("B1", It.IsAny<CancellationToken>())).ReturnsAsync(true);
            stocks.Setup(r => r.Exists("LSE:BAD", It.IsAny<CancellationToken>())).ReturnsAsync(false);

            var handler = new AddTradeCommandHandler(brokers.Object, stocks.Object, trades.Object, summary.Object, uow.Object, mediator.Object);

            var cmd = new AddTradeCommand("LSE:BAD", "B1", "Sell", 200, 5);

            Func<Task> act = () => handler.Handle(cmd, CancellationToken.None);

            await act.Should().ThrowAsync<Exception>()
                .WithMessage("Invalid or inactive ticker: LSE:BAD");
        }
    }
}
