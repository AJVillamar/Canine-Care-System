using MediatR;
using Shared.Responses;
using Application.ModuleClient.Appointments.Dtos;

namespace Application.ModuleClient.Appointments.Queries.GetAppointmentsByWeek
{
    public class GetAppointmentsByWeekQuery : IRequest<ApiResult<List<AppointmentDto>>>
    {
        public DateOnly ReferenceDate { get; set; }
    }
}
