using Domain.ModuleAdministration.Professionals.Models;
using Domain.ModuleAdministration.Professionals.Builders;
using Application.ModuleAdministration.Professionals.Dtos;
using Application.ModuleAdministration.Professionals.Commands.CreateProfessional;

namespace Application.ModuleAdministration.Professionals.Mappers
{
    public class ProfessionalMapperApp
    {
        public Professional ToDomain(CreateProfessionalCommand command)
        {
            return new ProfessionalBuilder()
                .WithIdentification(command.Identification)
                .WithFirstName(command.FirstName)
                .WithLastName(command.LastName)
                .WithEmail(command.Email)
                .WithBirthDate(command.BirthDate)
                .WithYearsOfExperience(command.YearsOfExperience)
                .Build();
        }

        public ProfessionalDto ToDto(Professional domain)
        {
            return new ProfessionalDto
            {
                Id = domain.Id,
                Identification = domain.Identification.Value,
                FirstName = domain.FirstName.Value,
                LastName = domain.LastName.Value,
                Email = domain.Email.Value,
                BirthDate = domain.BirthDate,
                YearsOfExperience = domain.YearsOfExperience
            };
        }
    }
}
