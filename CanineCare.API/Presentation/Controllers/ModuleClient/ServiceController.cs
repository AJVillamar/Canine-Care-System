using MediatR;
using Shared.Responses;
using Microsoft.AspNetCore.Mvc;
using Application.ModuleClient.Services.Dtos;
using Application.ModuleClient.Services.Queries.GetAllServices;
using Application.ModuleClient.Services.Queries.GetServiceById;
using Application.ModuleClient.Services.Commands.CreateService;
using Application.ModuleClient.Services.Queries.GetAllSevicesType;

namespace Presentation.Controllers.ModuleClient
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ServiceController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<ApiResult>> Create([FromBody] CreateServiceCommand command)
        {
            var response = await _mediator.Send(command);
            return Ok(response);
        }
        
        [HttpGet("by-id/{id}")]
        public async Task<ActionResult<ApiResult<ServiceDto>>> GetById([FromRoute] Guid id)
        {
            var query = new GetServiceByIdQuery { Id = id };
            var response = await _mediator.Send(query);
            return Ok(response);
        }

        [HttpGet]
        public async Task<ActionResult<ApiResult<List<ServiceDto>>>> GetAll()
        {
            var response = await _mediator.Send(new GetAllServicesQuery());
            return Ok(response);
        }


        [HttpGet("types")]
        public async Task<ActionResult<ApiResult<List<ServiceTypeDto>>>> GetAllType()
        {
            var response = await _mediator.Send(new GetAllSevicesTypeQuery());
            return Ok(response);
        }
    }
}
