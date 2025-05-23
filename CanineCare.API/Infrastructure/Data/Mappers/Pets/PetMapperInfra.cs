using Domain.ModuleClient.Pets.Enums;
using Domain.ModuleClient.Pets.Models;
using Infrastructure.Data.Entities.Pet;
using Domain.ModuleClient.Pets.Builders;
using Infrastructure.Data.Mappers.People;

namespace Infrastructure.Data.Mappers.Pets
{
    public class PetMapperInfra
    {
        private readonly BreedMapperInfra _breedMapper;
        private readonly OwnerMapperInfra _ownerMapper;
        private readonly PetExtraInfoMapperInfra _extraInfoMapper;

        public PetMapperInfra(
            BreedMapperInfra breedMapper,
            OwnerMapperInfra ownerMapper,
            PetExtraInfoMapperInfra extraInfoMapper )
        {
            _breedMapper = breedMapper;
            _ownerMapper = ownerMapper;
            _extraInfoMapper = extraInfoMapper;
        }

        public Pet ToDomain(Guid id, string name, Guid ownerId)
        {
            return new PetBuilder()
                .WithId(id)
                .WithName(name)
                .WithOwner(_ownerMapper.ToDomain(ownerId))
                .BuildBasic();
        }

        public Pet ToDomain(PetEntity entity)
        {
            return new PetBuilder()
                .WithId(entity.Id)
                .WithName(entity.Name)
                .WithBreed(_breedMapper.ToDomain(entity.Breed))
                .WithBirthDate(entity.BirthDate)
                .WithSex(entity.Sex)
                .WithColor(entity.Color)
                .WithWeight(entity.Weight)
                .WithExtraInfo(_extraInfoMapper.ToDomian(entity.PetExtraInfo))
                .WithOwner(_ownerMapper.ToDomain(entity.Owner))
                .Build();
        }

        public PetEntity ToEntity(PetEntity entity, Pet domain)
        {
            entity.Name = domain.Name;
            entity.BreedId = domain.Breed.Id;
            entity.BirthDate = domain.BirthDate;
            entity.Sex = domain.Sex.GetSpanishValue();
            entity.Color = domain.Color;
            entity.Weight = domain.Weight.Value;
            entity.PetExtraInfo = _extraInfoMapper.ToEntity(entity.PetExtraInfo, domain.ExtraInfo);
            entity.UpdatedAt = DateTime.Now;
            return entity;
        }

        public PetEntity ToEntity(Pet domain)
        {

            return new PetEntity
            {
                Id = domain.Id,
                Name = domain.Name,
                BreedId = domain.Breed.Id,
                BirthDate = domain.BirthDate,
                Sex = domain.Sex.GetSpanishValue(),
                Color = domain.Color,
                Weight = domain.Weight.Value
            };
        }
    }
}
