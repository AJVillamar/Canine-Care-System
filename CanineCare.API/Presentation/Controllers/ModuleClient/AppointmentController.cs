using Application.ModuleClient.Appointments.Commands.CancelAppointment;
using Application.ModuleClient.Appointments.Commands.CreateAppointment;
using Application.ModuleClient.Appointments.Commands.UpdateAppointment;
using Application.ModuleClient.Appointments.Dtos;
using Application.ModuleClient.Appointments.Queries.GetAllAppointments;
using Application.ModuleClient.Appointments.Queries.GetAppointmentById;
using Application.ModuleClient.Appointments.Queries.GetAppointmentHours;
using Application.ModuleClient.Appointments.Queries.GetAppointmentsByWeek;
using Application.ModuleClient.Appointments.Queries.SearchAppointments;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shared.Responses;

namespace Presentation.Controllers.ModuleClient
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AppointmentController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<ApiResult>> Create([FromBody] CreateAppointmentCommand command)
        {
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpPut]
        public async Task<ActionResult<ApiResult>> Update([FromBody] UpdateAppointmentCommand command)
        {
            var response = await _mediator.Send(command);
            return Ok(response);
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResult>> Cancel([FromRoute] Guid id)
        {
            var command = new CancelAppointmentCommand { Id = id };
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpGet]
        public async Task<ActionResult<ApiResult>> GetAll()
        {
            var response = await _mediator.Send(new GetAllAppointmentsQuery());
            return Ok(response);
        }

        [HttpGet("by-id/{id}")]
        public async Task<ActionResult<ApiResult<AppointmentDto>>> GetById([FromRoute] Guid id)
        {
            var query = new GetAppointmentByIdQuery { Id = id };
            var response = await _mediator.Send(query);
            return Ok(response);
        }

        [HttpPost("search")]
        public async Task<ActionResult<ApiResult<List<AppointmentDto>>>> SearchAppointments([FromBody] SearchAppointmentsQuery query)
        {
            var response = await _mediator.Send(query);
            return Ok(response);
        }

        [HttpGet("hours")]
        public async Task<ActionResult<ApiResult<List<AppointmentHourDto>>>> GetAppointmentHours()
        {
            var response = await _mediator.Send(new GetAppointmentHoursQuery());
            return Ok(response);
        }

        [HttpGet("week")]
        public async Task<ActionResult<ApiResult<List<AppointmentDto>>>> GetAppointmentsByWeek([FromQuery] DateOnly date)
        {
            var query = new GetAppointmentsByWeekQuery { ReferenceDate = date };
            var response = await _mediator.Send(query);
            return Ok(response);
        }
    }
}
