using Infrastructure.Data.Entities.People;
using Domain.ModuleAdministration.Professionals.Models;
using Domain.ModuleAdministration.Professionals.Builders;

namespace Infrastructure.Data.Mappers.People
{
    public class ProfessionalMapperInfra
    {
        public Professional ToDomain(ProfessionalEntity entity)
        {
            return new ProfessionalBuilder()
                .WithId(entity.PersonId)
                .WithIdentification(entity.Person.Identification)
                .WithFirstName(entity.Person.FirstName)
                .WithLastName(entity.Person.LastName)
                .WithEmail(entity.Person.Email)
                .WithBirthDate(entity.BirthDate)
                .WithYearsOfExperience(entity.YearsOfExperience)
                .Build();
        }

        public ProfessionalEntity ToEntity(Professional domain)
        {
            return new ProfessionalEntity
            {
                Id = Guid.NewGuid(),
                PersonId = domain.Id,
                BirthDate = domain.BirthDate,
                YearsOfExperience = domain.YearsOfExperience,
            };
        }
    }
}
