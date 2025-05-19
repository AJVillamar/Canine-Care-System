using MediatR;
using Shared.Responses;
using Microsoft.AspNetCore.Mvc;
using Application.ModuleIdentity.Roles.Commands.CreateDefaultRoles;

namespace Presentation.Controllers.ModuleIdentity
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IMediator _mediator;

        public RoleController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("create-defaults")]
        public async Task<ActionResult<ApiResult>> CreateDefaults()
        {
            var response = await _mediator.Send(new CreateDefaultRolesCommand());
            return Ok(response);
        }
    }
}
