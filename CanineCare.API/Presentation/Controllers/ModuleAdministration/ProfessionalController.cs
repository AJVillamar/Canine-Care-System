﻿using MediatR;
using Shared.Responses;
using Microsoft.AspNetCore.Mvc;
using Application.ModuleAdministration.Professionals.Dtos;
using Application.ModuleAdministration.Professionals.Commands.CreateProfessional;
using Application.ModuleAdministration.Professionals.Queries.GetAllProfessionals;
using Application.ModuleAdministration.Professionals.Queries.GetProfessionalById;

namespace Presentation.Controllers.ModuleAdministration
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfessionalController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProfessionalController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<ApiResult>> Create([FromBody] CreateProfessionalCommand command)
        {
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpGet]
        public async Task<ActionResult<ApiResult<List<ProfessionalDto>>>> GetAll()
        {
            var response = await _mediator.Send(new GetAllProfessionalsQuery());
            return Ok(response);
        }

        [HttpGet("by-id/{professionalId}")]
        public async Task<ActionResult<ApiResult<ProfessionalDto>>> GetById([FromRoute] Guid professionalId)
        {
            var command = new GetProfessionalByIdQuery { ProfessionalId = professionalId };
            var response = await _mediator.Send(command);
            return Ok(response);
        }
    }
}
