using MediatR;
using Shared.Responses;
using Microsoft.AspNetCore.Mvc;
using Application.ModuleClient.Owners.Dtos;
using Application.ModuleClient.Owners.Commands.CreateOwner;
using Application.ModuleClient.Owners.Commands.UpdateOwner;
using Application.ModuleClient.Owners.Queries.GetAllOwners;
using Application.ModuleClient.Owners.Queries.GetOwnerById;
using Application.ModuleClient.Owners.Queries.GetOwnerByIdentification;

namespace Presentation.Controllers.ModuleClient
{
    [Route("api/[controller]")]
    [ApiController]
    public class OwnerController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OwnerController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<ApiResult>> Create([FromBody] CreateOwnerCommand command)
        {
            var response = await _mediator.Send(command);
            return Ok(response);
        }


        [HttpPut]
        public async Task<ActionResult<ApiResult>> Update([FromBody] UpdateOwnerCommand command)
        {
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpGet("by-id/{id}")]
        public async Task<ActionResult<ApiResult<List<OwnerDto>>>> GetById([FromRoute] Guid id)
        {
            var query = new GetOwnerByIdQuery { Id = id };
            var response = await _mediator.Send(query);
            return Ok(response);
        }


        [HttpGet("by-identification/{identification}")]
        public async Task<ActionResult<ApiResult<List<OwnerDto>>>> GetByIdentification([FromRoute] string identification)
        {
            var query = new GetOwnerByIdentificationQuery { Identification = identification };
            var response = await _mediator.Send(query);
            return Ok(response);
        }


        [HttpGet]
        public async Task<ActionResult<ApiResult<List<OwnerDto>>>> GetAll()
        {
            var response = await _mediator.Send(new GetAllOwnersQuery());
            return Ok(response);
        }
    }
}
