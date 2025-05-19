using Domain.ModuleClient.Pets.Models;
using Infrastructure.Data.Entities.Pet;
using Domain.ModuleClient.Pets.Builders;

namespace Infrastructure.Data.Mappers.Pets
{
    public class PetExtraInfoMapperInfra
    {
        public PetExtraInfo ToDomian(PetExtraInfoEntity entity)
        {
            return new PetExtraInfoBuilder()
                .WithId(entity.Id)
                .WithAllergies(entity.Allergies)
                .WithConditions(entity.PreExistingConditions)
                .WithCareInstructions(entity.SpecialCareInstructions)
                .WithFeedingNotes(entity.FeedingNotes)
                .Build();
        }

        public PetExtraInfoEntity ToEntity(PetExtraInfoEntity entity, PetExtraInfo domain)
        {
            entity.Allergies = domain.Allergies;
            entity.PreExistingConditions = domain.PreExistingConditions;
            entity.SpecialCareInstructions = domain.SpecialCareInstructions;
            entity.FeedingNotes = domain.FeedingNotes;
            entity.UpdatedAt = DateTime.Now;
            return entity;
        }

        public PetExtraInfoEntity ToEntity(PetExtraInfo domain)
        {
            return new PetExtraInfoEntity
            {
                Id = domain.Id,
                Allergies = domain.Allergies,
                PreExistingConditions = domain.PreExistingConditions,
                SpecialCareInstructions = domain.SpecialCareInstructions,
                FeedingNotes = domain.FeedingNotes
            };
        }
    }
}
