using MediatR;
using Shared.Responses;
using Microsoft.AspNetCore.Mvc;
using Application.ModuleClient.Pets.Dtos;
using Application.ModuleClient.Pets.Commands.CreatePet;
using Application.ModuleClient.Pets.Commands.UpdatePet;
using Application.ModuleClient.Pets.Queries.GetAllPets;
using Application.ModuleClient.Pets.Queries.GetPetsByOwnerId;
using Application.ModuleClient.Pets.Queries.SearchPetsByNameOrOwnerName;

namespace Presentation.Controllers.ModuleClient
{
    [Route("api/[controller]")]
    [ApiController]
    public class PetController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PetController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<ApiResult>> Create([FromBody] CreatePetCommand command)
        {
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpPut]
        public async Task<ActionResult<ApiResult>> Update([FromBody] UpdatePetCommand command)
        {
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpGet]
        public async Task<ActionResult<ApiResult<List<PetDto>>>> GetAll()
        {
            var response = await _mediator.Send(new GetAllPetsQuery());
            return Ok(response);
        }

        [HttpGet("owner/{ownerId}")]
        public async Task<ActionResult<ApiResult<List<PetDto>>>> GetByOwnerId([FromRoute] Guid ownerId)
        {
            var query = new GetPetsByOwnerIdQuery { OwnerId = ownerId };
            var response = await _mediator.Send(query);
            return Ok(response);
        }

        [HttpGet("search/{term}")]
        public async Task<ActionResult<ApiResult<List<PetDto>>>> Search([FromRoute] string term)
        {
            var query = new SearchPetsByNameOrOwnerNameQuery { SearchTerm = term };
            var response = await _mediator.Send(query);
            return Ok(response);
        }
    }
}
