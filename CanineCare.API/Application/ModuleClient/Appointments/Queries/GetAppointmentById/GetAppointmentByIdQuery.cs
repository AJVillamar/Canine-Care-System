using MediatR;
using Shared.Responses;
using Application.ModuleClient.Appointments.Dtos;

namespace Application.ModuleClient.Appointments.Queries.GetAppointmentById
{
    public class GetAppointmentByIdQuery : IRequest<ApiResult<AppointmentDto>>
    {
        public Guid Id { get; set; }
    }
}
