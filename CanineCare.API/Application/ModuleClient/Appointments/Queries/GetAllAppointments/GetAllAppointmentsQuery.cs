using MediatR;
using Shared.Responses;
using Application.ModuleClient.Appointments.Dtos;

namespace Application.ModuleClient.Appointments.Queries.GetAllAppointments
{
    public class GetAllAppointmentsQuery : IRequest<ApiResult<List<AppointmentDto>>> { }
}
