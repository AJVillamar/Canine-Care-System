namespace Application.ModuleClient.Pets.Dtos
{
    public class PetExtraInfoQueryDto
    {
        public Guid Id { get; set; }

        public string? Allergies { get; set; }

        public string? PreExistingConditions { get; set; }

        public string? SpecialCareInstructions { get; set; }

        public string? FeedingNotes { get; set; }
    }
}
