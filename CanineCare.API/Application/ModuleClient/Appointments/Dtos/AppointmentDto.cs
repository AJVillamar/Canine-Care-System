using Application.ModuleClient.Pets.Dtos;
using Application.ModuleClient.Services.Dtos;
using Application.ModuleAdministration.Professionals.Dtos;

namespace Application.ModuleClient.Appointments.Dtos
{
    public class AppointmentDto
    {
        public Guid Id { get; set; }

        public PetDto? Pet { get; set; }

        public ServiceDto? Service { get; set; }

        public DateOnly Date { get; set; }

        public TimeOnly Time { get; set; }

        public ProfessionalDto? Professional { get; set; }

        public string? Reason { get; set; }

        public string? Status { get; set; }
    }
}
