using Application.ModuleClient.Pets.Dtos;
using MediatR;
using Shared.Responses;

namespace Application.ModuleClient.Pets.Commands.UpdatePet
{
    public class UpdatePetCommand : IRequest<ApiResult>
    {
        public Guid Id { get; set; }

        public string? Name { get; set; }

        public Guid BreedId { get; set; }

        public DateTime BirthDate { get; set; }

        public string? Sex { get; set; }

        public string? Color { get; set; }

        public double Weight { get; set; }

        public PetExtraInfoCommandDto? PetExtraInfo { get; set; }
    }
}
