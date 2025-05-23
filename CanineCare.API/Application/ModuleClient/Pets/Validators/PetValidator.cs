using Domain.Shared.Exceptions;
using Domain.ModuleClient.Owners.Repositories;
using Application.ModuleClient.Pets.Commands.CreatePet;
using Application.ModuleClient.Pets.Commands.UpdatePet;
using Domain.ModuleClient.Pets.Repositories;

namespace Application.ModuleClient.Pets.Validators
{
    public class PetValidator
    {
        private readonly IBreedRepository _breedRepository;
        private readonly IOwnerRepository _ownerRepository;
        private readonly IPetRepository _petRepository;

        public PetValidator(
            IBreedRepository breedRepository, 
            IOwnerRepository ownerRepository,
            IPetRepository petRepository )
        {
            _breedRepository = breedRepository;
            _ownerRepository = ownerRepository;
            _petRepository = petRepository;
        }

        public async Task ValidateCreateAsync(CreatePetCommand command)
        {
            if (await _breedRepository.GetByIdAsync(command.BreedId) == null)
                throw new NotFoundException("Raza");


            if (await _ownerRepository.GetByIdAsync(command.OwnerId) == null)
                throw new NotFoundException("Cliente");
        }

        public async Task ValidateUpdateAsync(UpdatePetCommand command)
        {
            if (await _petRepository.GetByIdAsync(command.Id) == null)
                throw new NotFoundException("Mascota");

            if (await _breedRepository.GetByIdAsync(command.BreedId) == null)
                throw new NotFoundException("Raza");
        }

        public async Task ValidateGetByIdAsync(Guid id)
        {
            if (await _petRepository.GetByIdAsync(id) == null)
                throw new NotFoundException("Mascota");
        }
    }
}
 