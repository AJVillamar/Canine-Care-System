using Domain.ModuleClient.Pets.Builders;
using Application.ModuleClient.Breeds.Dtos;
using Application.ModuleClient.Breeds.Commands.CreateBreed;
using Domain.ModuleClient.Pets.Enums;
using Domain.ModuleClient.Pets.Models;

namespace Application.ModuleClient.Breeds.Mappers
{
    public class BreedMapperApp
    {
        public Breed ToDomain(Guid id)
        {
            return new BreedBuilder().WithId(id).BuildReference();
        }

        public Breed ToDomain(CreateBreedCommand command) 
        {
            return new BreedBuilder().WithName(command.Name).Build();
        }

        public BreedDto ToDto(Breed domain)
        {
            return new BreedDto
            {
                Id = domain.Id,
                Name = domain.Name,
                Specie = domain.Species.GetSpanishValue()
            };
        }
    }
}
