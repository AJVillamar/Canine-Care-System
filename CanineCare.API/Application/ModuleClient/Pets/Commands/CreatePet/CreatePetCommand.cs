using MediatR;
using Shared.Responses;
using Application.ModuleClient.Pets.Dtos;

namespace Application.ModuleClient.Pets.Commands.CreatePet
{
    public class CreatePetCommand : IRequest<ApiResult>
    {
        public string? Name { get; set; }

        public Guid BreedId { get; set; }

        public DateTime BirthDate { get; set; }
        
        public string? Sex { get; set; }

        public string? Color { get; set; }

        public double Weight { get; set; }

        public PetExtraInfoCommandDto? PetExtraInfo { get; set; }

        public Guid OwnerId { get; set; }
    }
}
 