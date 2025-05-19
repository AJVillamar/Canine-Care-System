using MediatR;
using Shared.Responses;

namespace Application.ModuleAdministration.Professionals.Commands.CreateProfessional
{
    public class CreateProfessionalCommand : IRequest<ApiResult>
    {
        public string? Identification { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? Email { get; set; }

        public DateTime BirthDate { get; set; }

        public int YearsOfExperience { get; set; }
    }
}
