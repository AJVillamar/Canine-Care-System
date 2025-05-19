using Domain.ModuleAdministration.Admins.Models;
using Domain.ModuleAdministration.Professionals.Models;
using Domain.ModuleClient.Owners.Models;
using Domain.ModuleIdentity.Users.Builders;
using Domain.ModuleIdentity.Users.Models;
using Infrastructure.Data.Entities.Identity;
using Infrastructure.Data.Entities.People;

namespace Infrastructure.Data.Mappers.People
{
    public class PersonMapperInfra
    {
        public User ToDomain(PersonEntity entity)
        {
            return new UserBuilder()
                .WithId(entity.Id)
                .WithIdentification(entity.Identification)
                .WithFirstName(entity.FirstName)
                .WithLastName(entity.LastName)
                .WithEmail(entity.Email)
                .Build();
        }

        private PersonEntity MapToEntity(dynamic model)
        {
            return new PersonEntity
            {
                Id = model.Id,
                Identification = model.Identification.Value,
                FirstName = model.FirstName.Value,
                LastName = model.LastName.Value,
                Email = model.Email != null ? model.Email.Value : null
            };
        }

        public PersonEntity ToEntity(Admin model) => MapToEntity(model);

        public PersonEntity ToEntity(Owner model) => MapToEntity(model);

        public PersonEntity ToEntity(Professional model) => MapToEntity(model);

        public UserEntity ToEntity(Guid personId, string password)
        {
            return new UserEntity
            {
                Id = new Guid(),
                PersonId = personId,
                Password = password
            };
        }
    }
}
