using MediatR;
using Shared.Responses;
using Application.ModuleClient.Appointments.Dtos;

namespace Application.ModuleClient.Appointments.Queries.SearchAppointments
{
    public class SearchAppointmentsQuery : IRequest<ApiResult<List<AppointmentDto>>>
    {
        public DateOnly? Date { get; set; }

        public string? Query { get; set; }
    }
}
