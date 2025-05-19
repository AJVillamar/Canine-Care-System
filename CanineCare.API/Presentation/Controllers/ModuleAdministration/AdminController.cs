using MediatR;
using Shared.Responses;
using Microsoft.AspNetCore.Mvc;
using Application.ModuleAdministration.Admins.Commands.CreatetDefaultAdmin;

namespace Presentation.Controllers.ModuleAdministration
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AdminController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("create-default")]
        public async Task<ActionResult<ApiResult>> Create()
        {
            var response = await _mediator.Send(new CreateDefaultAdminCommand());
            return Ok(response);
        }
    }
}
