using Asp.Versioning;
using LSE.Application.DTOs;
using LSE.Application.Queries.GetLatestPrice;
using LSE.Application.Queries.GetStockPriceByTickers;
using LSE.Application.Queries.Stocks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LSE.API.Controllers
{
    /// <summary>
    /// Manages stock price queries and stock listings for London Stock Exchange
    /// </summary>
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/stocks")]
    public class StocksController : ControllerBase
    {
        private readonly IMediator _mediator;

        /// <summary>
        /// Initializes a new instance of the <see cref="StocksController"/> class.
        /// </summary>
        /// <param name="mediator">MediatR instance for handling stock queries</param>
        public StocksController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Retrieves the latest average price for a specific stock ticker (Authenticated brokers only)
        /// </summary>
        /// <param name="ticker" example="LSE:VOD">Stock symbol in format EXCHANGE:TICKER (e.g., LSE:VOD, LSE:GLEN)</param>
        /// <returns>Latest price summary with average, high, low, and volume</returns>
        /// <response code="200">Latest price data</response>
        /// <response code="404">Stock ticker not found</response>
        /// <response code="400">Invalid ticker format</response>
        /// <response code="401">Unauthorized - JWT token required</response>
        [Authorize]
        [HttpGet("{ticker}")]
        [ProducesResponseType(typeof(TradeDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetLatestPrice(string ticker)
        {
            var result = await _mediator.Send(new GetLatestPriceQuery(ticker));
            return result == null ? NotFound() : Ok(result);
        }

        /// <summary>
        /// Batch retrieves latest prices for multiple stock tickers (Authenticated brokers only)
        /// </summary>
        /// <param name="tickers" example="['LSE:VOD', 'LSE:SHEL', 'LSE:HSBA']">Array of stock tickers</param>
        /// <returns>List of latest price summaries</returns>
        /// <response code="200">Batch price data</response>
        /// <response code="400">Empty or invalid ticker list  (Authenticated brokers only)</response>
        /// <response code="401">Unauthorized - JWT token required</response>
        [Authorize]
        [HttpPost("prices")]
        [ProducesResponseType(typeof(List<TradeDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetPrices([FromBody] List<string> tickers)
        {
            var result = await _mediator.Send(new GetPricesByTickersQuery(tickers));
            return Ok(result);
        }

        /// <summary>
        /// Retrieves complete list of available stocks (Authenticated brokers only)
        /// </summary>
        /// <returns>All available stock listings</returns>
        /// <response code="200">Complete stock list</response>
        /// <response code="401">Unauthorized - JWT token required</response>
        [Authorize]
        [HttpGet]
        [ProducesResponseType(typeof(List<StockListItemDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetAllStocks()
        {
            var stocks = await _mediator.Send(new GetStockListQuery());
            return Ok(stocks);
        }
    }
}
