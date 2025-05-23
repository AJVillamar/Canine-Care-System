using Domain.ModuleClient.Pets.Enums;
using Domain.ModuleClient.Pets.Models;
using Domain.ModuleClient.Pets.Builders;
using Application.ModuleClient.Pets.Dtos;
using Application.ModuleClient.Breeds.Mappers;
using Application.ModuleClient.Owners.Mappers;
using Application.ModuleClient.Pets.Commands.CreatePet;
using Application.ModuleClient.Pets.Commands.UpdatePet;

namespace Application.ModuleClient.Pets.Mappers
{
    public class PetMapperApp
    {
        private readonly PetExtraInfoMapperApp _extraInfoMapper;
        private readonly BreedMapperApp _breedMapper;
        private readonly OwnerMapperApp _ownerMapper;

        public PetMapperApp(
            PetExtraInfoMapperApp extraInfoMapper,
            BreedMapperApp breedMapper,
            OwnerMapperApp ownerMapper )
        {
            _extraInfoMapper = extraInfoMapper;
            _breedMapper = breedMapper;
            _ownerMapper = ownerMapper;
        }

        public Pet ToDomain(Guid id)
        {
            return new PetBuilder()
                .WithId(id)
                .BuildMinimal();
        }


        public Pet ToDomain(CreatePetCommand command)
        {
            var builder = new PetBuilder()
                .WithName(command.Name)
                .WithBreed(_breedMapper.ToDomain(command.BreedId))
                .WithBirthDate(command.BirthDate)
                .WithSex(command.Sex)
                .WithColor(command.Color)
                .WithWeight(command.Weight);

            if (command.PetExtraInfo is not null)
            {
                var extraInfo = _extraInfoMapper.ToDomain(command.PetExtraInfo);
                builder.WithExtraInfo(extraInfo);
            }

            builder.WithOwner(_ownerMapper.ToDomain(command.OwnerId));
            return builder.Build();
        }

        public Pet ToDomain(UpdatePetCommand command, Pet domain)
        {
            domain.UpdateName(command.Name);
            domain.UpdateBreed(_breedMapper.ToDomain(command.BreedId));
            domain.UpdateBirthDate(command.BirthDate);
            domain.UpdateSex(command.Sex);
            domain.UpdateColor(command.Color);
            domain.UpdateWeight(command.Weight);
            
            if (command.PetExtraInfo is not null)
            {
                var extraInfo = _extraInfoMapper.ToDomain(command.PetExtraInfo);
                domain.UpdateExtraInfo(extraInfo);
            }

            return domain;
        }

        public PetDto ToDto(Pet domain)
        {
            return new PetDto
            {
                Id = domain.Id,
                Name = domain.Name,
                Breed = _breedMapper.ToDto(domain.Breed),
                BirthDate = domain.BirthDate,
                Sex = domain.Sex.GetSpanishValue(),
                Color = domain.Color,
                Weight = domain.Weight.Value,
                PetExtraInfo = _extraInfoMapper.ToDto(domain.ExtraInfo),
                Owner = _ownerMapper.ToDto(domain.Owner)
            };
        }

        public PetDto ToDto(Guid id, string name, Guid ownerId)
        {
            return new PetDto
            {
                Id = id,
                Name = name,
                Owner = _ownerMapper.ToDto(ownerId)
            };
        }
    }
}
