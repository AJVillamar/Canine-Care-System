using Infrastructure.Data.Entities.Pet;
using Domain.ModuleClient.Pets.Builders;
using Domain.ModuleClient.Pets.Enums;
using Domain.ModuleClient.Pets.Models;

namespace Infrastructure.Data.Mappers.Pets
{
    public class BreedMapperInfra
    {
        public Breed ToDomain(BreedEntity entity)
        {
            return new BreedBuilder()
                .WithId(entity.Id)
                .WithName(entity.Name)
                .WithSpecies(entity.Species)
                .Build();
        }

        public BreedEntity ToEntity(Breed entity)
        {
            return new BreedEntity
            {
                Id = entity.Id,
                Name = entity.Name,
                Species = entity.Species.GetSpanishValue()
            };
        }
    }
}
