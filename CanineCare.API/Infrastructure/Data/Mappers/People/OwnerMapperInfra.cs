using Domain.ModuleClient.Owners.Models;
using Domain.ModuleClient.Owners.Builders;
using Infrastructure.Data.Entities.People;

namespace Infrastructure.Data.Mappers.People
{
    public class OwnerMapperInfra
    {
        public Owner ToDomain(OwnerEntity entity)
        {
            return new OwnerBuilder()
                .WithId(entity.PersonId)
                .WithIdentification(entity.Person.Identification)
                .WithFirstName(entity.Person.FirstName)
                .WithLastName(entity.Person.LastName)
                .WithEmail(entity.Person.Email)
                .WithPhone(entity.Phone)
                .WithAddress(entity.Address)
                .Build();
        }

        public OwnerEntity ToEntity(Owner domain)
        {
            return new OwnerEntity
            {
                Id = Guid.NewGuid(),
                PersonId = domain.Id,
                Phone = domain.Phone.Value,
                Address = domain.Address,
            };
        }

        public OwnerEntity ToEntity(OwnerEntity entity, Owner domain)
        {
            entity.Person!.FirstName = domain.FirstName.Value;
            entity.Person.LastName = domain.LastName.Value;
            entity.Person.Email = domain.Email.Value;
            entity.Person.UpdatedAt = DateTime.Now;

            entity.Phone = domain.Phone.Value;
            entity.Address = domain.Address;
            entity.Person.UpdatedAt = DateTime.Now;

            return entity;
        }
    }
}
