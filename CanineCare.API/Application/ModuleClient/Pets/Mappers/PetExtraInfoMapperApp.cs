using Domain.ModuleClient.Pets.Models;
using Domain.ModuleClient.Pets.Builders;
using Application.ModuleClient.Pets.Dtos;

namespace Application.ModuleClient.Pets.Mappers
{
    public class PetExtraInfoMapperApp
    {
        public PetExtraInfo ToDomain(PetExtraInfoCommandDto dto)
        {
            return new PetExtraInfoBuilder()
                .WithAllergies(dto.Allergies)
                .WithConditions(dto.PreExistingConditions)
                .WithCareInstructions(dto.SpecialCareInstructions)
                .WithFeedingNotes(dto.FeedingNotes)
                .Build();
        }

        public PetExtraInfo ToDomain(PetExtraInfoCommandDto dto, PetExtraInfo domain)
        {
            domain.UpdateAllergies(dto.Allergies);
            domain.UpdatePreExistingConditions(dto.PreExistingConditions);
            domain.UpdateSpecialCareInstructions(dto.SpecialCareInstructions);
            domain.UpdateFeedingNotes(dto.FeedingNotes);
            return domain;
        }

        public PetExtraInfoQueryDto ToDto(PetExtraInfo domain)
        {
            return new PetExtraInfoQueryDto
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
