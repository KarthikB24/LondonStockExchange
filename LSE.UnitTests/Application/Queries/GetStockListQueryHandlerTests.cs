using FluentAssertions;
using LSE.Application.Abstraction.Stock;
using LSE.Application.Queries.Stocks;
using LSE.Application.DTOs;
using Moq;

namespace LSE.UnitTests.Application.Queries
{
    public class GetStockListQueryHandlerTests
    {
        [Fact]
        public async Task Handle_ShouldReturnStockList_WhenStocksExist()
        {
            // Arrange
            var mockRepo = new Mock<IStockRepository>();

            var fakeStocks = new List<StockListItemDto>
            {
                new StockListItemDto
                {
                    Ticker = "LSE:HSBA",
                    CompanyName = "HSBC Holdings plc"
                },
                new StockListItemDto
                {
                    Ticker = "LSE:BARC",
                    CompanyName = "Barclays plc"
                }
            };

            mockRepo
                .Setup(r => r.GetAllStocksAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(fakeStocks);

            var handler = new GetStockListQueryHandler(mockRepo.Object);

            // Act
            var result = await handler.Handle(new GetStockListQuery(), CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Should().HaveCount(2);
            result.First().Ticker.Should().Be("LSE:HSBA");

            mockRepo.Verify(r => r.GetAllStocksAsync(It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}
