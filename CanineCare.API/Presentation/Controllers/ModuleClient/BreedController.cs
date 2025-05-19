using MediatR;
using Shared.Responses;
using Microsoft.AspNetCore.Mvc;
using Application.ModuleClient.Breeds.Dtos;
using Application.ModuleClient.Breeds.Commands.CreateBreed;
using Application.ModuleClient.Breeds.Queries.GetAllBreeds;
using Application.ModuleClient.Breeds.Commands.CreateDefaultBreeds;

namespace Presentation.Controllers.ModuleClient
{
    [Route("api/[controller]")]
    [ApiController]
    public class BreedController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BreedController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<ApiResult>> Create([FromBody] CreateBreedCommand command)
        {
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpPost("default")]
        public async Task<ActionResult<ApiResult>> CreateDefaults()
        {
            var response = await _mediator.Send(new CreateDefaultBreedsCommand());
            return Ok(response);
        }

        [HttpGet]
        public async Task<ActionResult<ApiResult<List<BreedDto>>>> GetAll()
        {
            var response = await _mediator.Send(new GetAllBreedsQuery());
            return Ok(response);
        }
    }
}
