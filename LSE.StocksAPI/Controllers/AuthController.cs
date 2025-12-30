using LSE.Application.Abstraction.Broker;
using LSE.Application.Queries.Auth; 
using LSE.Application.DTOs.BrokerAuth;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace LSE.StocksAPI.Controllers
{
    /// <summary>
    /// Handles broker authentication and authorization for London Stock Exchange API
    /// </summary>
    [ApiController]
    [Route("api/v1/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator; 
        
        /// <summary>
        /// Initializes a new instance of the <see cref="AuthController"/> class.
        /// </summary>
        /// <param name="mediator">MediatR instance for sending commands/queries</param>
        public AuthController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        /// <summary>
        /// Authenticates a broker and returns JWT token
        /// </summary>
        /// <param name="query">
        /// Broker login credentials. Example:
        /// { "username": "jpm.trader@lse.com", "password": "test123" }
        /// </param>
        /// <returns>JWT token on success</returns>
        /// <response code="200">Valid credentials - JWT token returned</response>
        /// <response code="401">Invalid username or password</response> 
        [AllowAnonymous]
        [HttpPost("BrokerLogin")]
        [ProducesResponseType(typeof(BrokerLoginResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Login([FromBody] BrokerLoginQuery query)
        {
            var result = await _mediator.Send(query);

            if (result == null)
                return Unauthorized("Invalid credentials");

            return Ok(result);
        }
    }
}
