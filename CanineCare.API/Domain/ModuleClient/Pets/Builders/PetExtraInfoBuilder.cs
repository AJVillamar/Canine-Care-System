using Domain.ModuleClient.Pets.Models;

namespace Domain.ModuleClient.Pets.Builders
{
    public class PetExtraInfoBuilder
    {
        private Guid? _id;
        private string? _allergies;
        private string? _conditions;
        private string? _careInstructions;
        private string? _feedingNotes;

        public PetExtraInfoBuilder() { }

        public PetExtraInfoBuilder WithId(Guid id) => SetProperty(ref _id, id);
        public PetExtraInfoBuilder WithAllergies(string allergies) => SetProperty(ref _allergies, allergies);
        public PetExtraInfoBuilder WithConditions(string conditions) => SetProperty(ref _conditions, conditions);
        public PetExtraInfoBuilder WithCareInstructions(string careInstructions) => SetProperty(ref _careInstructions, careInstructions);
        public PetExtraInfoBuilder WithFeedingNotes(string feedingNotes) => SetProperty(ref _feedingNotes, feedingNotes);

        public PetExtraInfo Build()
        {
            return PetExtraInfo.Create(
                _id,
                _allergies!,
                _conditions!,
                _careInstructions!,
                _feedingNotes!
            );
        }

        private PetExtraInfoBuilder SetProperty<T>(ref T field, T value)
        {
            field = value;
            return this;
        }
    }
}
