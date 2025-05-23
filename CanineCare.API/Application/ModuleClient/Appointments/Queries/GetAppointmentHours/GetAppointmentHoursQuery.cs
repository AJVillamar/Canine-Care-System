using Application.ModuleClient.Appointments.Dtos;
using MediatR;
using Shared.Responses;

namespace Application.ModuleClient.Appointments.Queries.GetAppointmentHours
{
    public class GetAppointmentHoursQuery : IRequest<ApiResult<List<AppointmentHourDto>>> { }
}
