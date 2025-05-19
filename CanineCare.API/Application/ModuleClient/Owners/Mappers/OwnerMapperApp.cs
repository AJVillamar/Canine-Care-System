using Domain.ModuleClient.Owners.Models;
using Domain.ModuleClient.Owners.Builders;
using Application.ModuleClient.Owners.Dtos;
using Application.ModuleClient.Owners.Commands.CreateOwner;
using Application.ModuleClient.Owners.Commands.UpdateOwner;

namespace Application.ModuleClient.Owners.Mappers
{
    public class OwnerMapperApp
    {
        public Owner ToDomain(Guid id)
        {
            return new OwnerBuilder()
                .WithId(id)
                .BuildReference();
        }

        public Owner ToDomain(CreateOwnerCommand command)
        {
            return new OwnerBuilder()
                .WithIdentification(command.Identification)
                .WithFirstName(command.FirstName)
                .WithLastName(command.LastName)
                .WithEmail(command.Email)
                .WithPhone(command.Phone)
                .WithAddress(command.Address)
                .Build();
        }

        public Owner ToDomain(UpdateOwnerCommand command, Owner domain)
        {
            domain.UpdateFirstName(command.FirstName);
            domain.UpdateLastName(command.LastName);
            if (!string.IsNullOrWhiteSpace(command.Email)) domain.UpdateEmail(command.Email);
            domain.UpdatePhone(command.Phone);
            domain.UpdateAddress(command.Address);
            return domain;
        }

        public OwnerDto ToDto(Owner domain)
        {
            return new OwnerDto
            {
                Id = domain.Id,
                Identification = domain.Identification.Value,
                FirstName = domain.FirstName.Value,
                LastName = domain.LastName.Value,
                Email = domain.Email?.Value,
                Phone = domain.Phone.Value,
                Address = domain.Address
            };
        }
    }
}
