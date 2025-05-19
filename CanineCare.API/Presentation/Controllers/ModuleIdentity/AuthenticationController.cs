using MediatR;
using Shared.Responses;
using Microsoft.AspNetCore.Mvc;
using Application.ModuleIdentity.Authentication.Commands.Login;

namespace Presentation.Controllers.ModuleIdentity
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthenticationController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("login")]
        public async Task<ActionResult<ApiResult<string>>> Login([FromBody] LoginCommand command)
        {
            var response = await _mediator.Send(command);
            return Ok(response);
        }

    }
}
