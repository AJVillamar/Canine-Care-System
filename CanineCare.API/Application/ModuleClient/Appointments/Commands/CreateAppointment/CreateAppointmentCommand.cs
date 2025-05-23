using MediatR;
using Shared.Responses;

namespace Application.ModuleClient.Appointments.Commands.CreateAppointment
{
    public class CreateAppointmentCommand : IRequest<ApiResult>
    {
        public Guid PetId { get; set; }

        public Guid ServiceId { get; set; }

        public DateOnly Date { get; set; }

        public TimeOnly Time { get; set; }

        public Guid ProfessionalId { get; set; }

        public string? Reason { get; set; }
    }
}
