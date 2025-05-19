using Application.ModuleClient.Breeds.Dtos;
using Application.ModuleClient.Owners.Dtos;

namespace Application.ModuleClient.Pets.Dtos
{
    public class PetDto
    {
        public Guid Id { get; set; }

        public string? Name { get; set; }

        public BreedDto Breed { get; set; }

        public DateTime BirthDate { get; set; }

        public string? Sex { get; set; }

        public string? Color { get; set; }

        public double Weight { get; set; }

        public PetExtraInfoQueryDto? PetExtraInfo { get; set; }

        public OwnerDto Owner { get; set; }
    }
}
