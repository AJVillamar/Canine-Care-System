using MediatR;
using Shared.Responses;

namespace Application.ModuleClient.Appointments.Commands.UpdateAppointment
{
    public class UpdateAppointmentCommand : IRequest<ApiResult>
    {
        public Guid Id { get; set; }

        public Guid ServiceId { get; set; }

        public DateOnly Date { get; set; }

        public TimeOnly Time { get; set; }

        public Guid ProfessionalId { get; set; }
    }
}
