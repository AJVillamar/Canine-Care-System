using MediatR;
using Shared.Responses;

namespace Application.ModuleClient.Appointments.Commands.CancelAppointment
{
    public class CancelAppointmentCommand : IRequest<ApiResult>
    {
        public Guid Id { get; set; }
    }
}
