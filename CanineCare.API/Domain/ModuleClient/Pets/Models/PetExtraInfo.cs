using Domain.Shared.Exceptions;

namespace Domain.ModuleClient.Pets.Models
{
    public class PetExtraInfo
    {
        public Guid Id { get; private set; }

        public string Allergies { get; private set; }

        public string PreExistingConditions { get; private set; }

        public string SpecialCareInstructions { get; private set; }

        public string FeedingNotes { get; private set; }

        private PetExtraInfo(Guid id, string allergies, string conditions, string careInstructions, string feedingNotes)
        {
            Id = id;
            Allergies = allergies;
            PreExistingConditions = conditions;
            SpecialCareInstructions = careInstructions;
            FeedingNotes = feedingNotes;

            Validate();
        }

        public static PetExtraInfo Create(Guid? id, string allergies, string conditions, string careInstructions, string feedingNotes)
        {
            return new PetExtraInfo(
                id ?? Guid.NewGuid(),   
                allergies, 
                conditions, 
                careInstructions, 
                feedingNotes
            );
        }
        public void UpdateAllergies(string allergies)
        {
            Allergies = allergies;
            Validate();
        }

        public void UpdatePreExistingConditions(string conditions)
        {
            PreExistingConditions = conditions;
            Validate();
        }

        public void UpdateSpecialCareInstructions(string careInstructions)
        {
            SpecialCareInstructions = careInstructions;
            Validate();
        }

        public void UpdateFeedingNotes(string feedingNotes)
        {
            FeedingNotes = feedingNotes;
            Validate();
        }
        private void Validate()
        {
            if (string.IsNullOrWhiteSpace(Allergies))
                throw new EmptyFieldException("Alergias");

            if (string.IsNullOrWhiteSpace(PreExistingConditions))
                throw new EmptyFieldException("Condiciones preexistentes");

            if (string.IsNullOrWhiteSpace(SpecialCareInstructions))
                throw new EmptyFieldException("Instrucciones de cuidado especial");

            if (string.IsNullOrWhiteSpace(FeedingNotes))
                throw new EmptyFieldException("Notas de alimentación");
        }
    }
}
