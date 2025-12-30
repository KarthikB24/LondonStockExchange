using FluentAssertions;
using LSE.Application.Abstraction.Stock;
using LSE.Application.Queries.GetLatestPrice;
using LSE.Application.Queries.GetLatestPricePerStock;
using LSE.Application.Queries.GetStockPriceByTickers;
using LSE.Application.DTOs;
using Moq;

namespace LSE.UnitTests.Application.Queries
{
    public class GetLatestPriceQueryHandlerTests
    {
        [Fact]
        public async Task Handle_ShouldReturnLatestPrice_WhenFound()
        {
            // Arrange
            var repoMock = new Mock<IStockRepository>();
            repoMock.Setup(r => r.GetLatestPriceAsync("LSE:VOD", It.IsAny<CancellationToken>()))
                .ReturnsAsync(new TradeDto
                {
                    Ticker = "LSE:VOD",
                    Price = 100,
                    PriceValueAt = DateTime.UtcNow
                });

            // Act
            var handler = new GetLatestPriceQueryHandler(repoMock.Object);

            var result = await handler.Handle(new GetLatestPriceQuery("LSE:VOD"), CancellationToken.None);

            // Assert
           
            Assert.Equal("LSE:VOD", result.Ticker);

;            result.Should().NotBeNull();
            result!.Ticker.Should().Be("LSE:VOD");
        }

        [Fact]
        public async Task Handle_ShouldReturnNull_WhenNotFound()
        {
            var repoMock = new Mock<IStockRepository>();
            repoMock.Setup(r => r.GetLatestPriceAsync("LSE:XXX", It.IsAny<CancellationToken>()))
                    .ReturnsAsync((TradeDto?)null);

            var handler = new GetLatestPriceQueryHandler(repoMock.Object);

            var result = await handler.Handle(new GetLatestPriceQuery("LSE:XXX"), CancellationToken.None);

            result.Should().BeNull();
        }

        [Fact]
        public async Task Handle_WhenInputNull()
        {
            var repoMock = new Mock<IStockRepository>();
            repoMock.Setup(r => r.GetLatestPriceAsync(null, It.IsAny<CancellationToken>()))
                    .ReturnsAsync((TradeDto?)null);

            var handler = new GetLatestPriceQueryHandler(repoMock.Object);

            var result = await handler.Handle(new GetLatestPriceQuery("LSE:XXX"), CancellationToken.None);

            result.Should().BeNull();
        }


        [Theory]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("   ")] 
        public void Validator_ShouldFail_WhenTickerInvalid(string ticker)
        {
            var validator = new GetLatestPriceQueryValidator();
            var result = validator.Validate(new GetLatestPriceQuery(ticker));

            result.IsValid.Should().BeFalse();
        }

        [Fact]
        public void Validator_ShouldPass_WhenTickerValid()
        {
            var validator = new GetLatestPriceQueryValidator();
            var result = validator.Validate(new GetLatestPriceQuery("LSE:BARC"));

            result.IsValid.Should().BeTrue();
        }
    }
}

