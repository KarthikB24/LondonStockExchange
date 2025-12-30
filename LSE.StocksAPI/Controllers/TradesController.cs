using Asp.Versioning;
using LSE.Application.Commands.Trades;
using LSE.Application.Queries.GetListOfTrades;
using LSE.Application.DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace LSE.API.Controllers
{
    /// <summary>
    /// Manages buy/sell trade operations and trade history for London Stock Exchange
    /// </summary>
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/trades")]
    public class TradesController : ControllerBase
    {
        private readonly IMediator _mediator;

        /// <summary>
        /// Initializes a new instance of the <see cref="TradesController"/> class.
        /// </summary>
        /// <param name="mediator">MediatR instance for trade commands/queries</param>
        public TradesController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        /// <summary>
        /// Executes a BUY trade order for a broker (Authenticated brokers only)
        /// </summary>
        /// <param name="dto" example="{
        ///   'ticker': 'LSE:VOD',
        ///   'brokerId': 'BRK-CITI',
        ///   'price': 250.5000,
        ///   'quantity': 100
        /// }">Buy trade details</param>
        /// <returns>Confirmation that BUY trade was recorded</returns>
        /// <response code="200">Trade successfully recorded</response>
        /// <response code="400">Invalid trade data</response>
        /// <response code="401">Unauthorized - JWT token required</response>
        /// [Authorize]
        [HttpPost("buy")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Buy([FromBody] AddTradeDto dto)
        {
            await _mediator.Send(new AddTradeCommand(
                dto.Ticker,
                dto.BrokerId,
                "BUY",
                dto.Price,
                dto.Quantity));

            return Ok("Trade BUY recorded");
        }

        /// <summary>
        /// Executes a SELL trade order for a broker (Authenticated brokers only)
        /// </summary>
        /// <param name="dto" example="{
        ///   'ticker': 'LSE:VOD',
        ///   'brokerId': 'BROKER123',
        ///   'price': 245.75,
        ///   'quantity': 50
        /// }">Sell trade details</param>
        /// <returns>Confirmation that SELL trade was recorded</returns>
        /// <response code="200">Trade successfully recorded</response>
        /// <response code="400">Invalid trade data</response>
        /// <response code="401">Unauthorized - JWT token required</response>
        [Authorize]
        [HttpPost("sell")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Sell([FromBody] AddTradeDto dto)
        {
            await _mediator.Send(new AddTradeCommand(
                dto.Ticker,
                dto.BrokerId,
                "SELL",
                dto.Price,
                dto.Quantity));

            return Ok("Trade SELL recorded");
        }

        /// <summary>
        /// Retrieves paginated list of all trades (Authenticated brokers only)
        /// </summary>
        /// <param name="page" example="1">Page number (1-based)</param>
        /// <param name="pageSize" example="50">Number of trades per page (default: 100, max: 1000)</param>
        /// <returns>Paginated trade history</returns>
        /// <response code="200">Paginated trades with metadata</response>
        /// <response code="401">Unauthorized - JWT token required</response>
        [Authorize]
        [HttpGet]
        [ProducesResponseType(typeof(TradeListItemDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllTrades(
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 100)
        {
            var result = await _mediator.Send(new GetAllTradesQuery(page, pageSize));
            return Ok(result);
        }
    }
}
