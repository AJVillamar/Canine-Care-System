using Domain.Shared.Exceptions;
using Domain.Shared.ValueObjects;
using Domain.ModuleClient.Owners.Repositories;
using Application.ModuleIdentity.Users.Validators;
using Application.ModuleClient.Owners.Commands.CreateOwner;
using Application.ModuleClient.Owners.Commands.UpdateOwner;

namespace Application.ModuleClient.Owners.Validators
{
    public class OwnerValidator
    {
        private readonly UserValidator _userValidator;
        private readonly IOwnerRepository _ownerRepository;

        public OwnerValidator(
            UserValidator userValidator,
            IOwnerRepository ownerRepository )
        {
            _userValidator = userValidator;
            _ownerRepository = ownerRepository;
        }

        public async Task ValidateCreateAsync(CreateOwnerCommand command)
        {
            await _userValidator.ValidateCreateAsync(
                identification: command.Identification, 
                email: command.Email);
        }

        public async Task ValidateUpdateAsync(UpdateOwnerCommand command)
        {
            if (await _ownerRepository.GetByIdAsync(command.Id) == null)
                throw new NotFoundException("El Dueño de la mascota");

            if(!string.IsNullOrWhiteSpace(command.Email))
                await _userValidator.ValidateUpdateAsync(email: command.Email);
        }

        public async Task ValidateGetByIdAsync(Guid id)
        {
            if (await _ownerRepository.GetByIdAsync(id) == null)
                throw new NotFoundException("El Dueño de la mascota");
        }

        public async Task ValidateGetByIdentificationAsync(string identification)
        {
            Identification.Create(identification);

            if( await _ownerRepository.GetByIdentificationAsync(identification) == null)
                throw new NotFoundException("El Dueño de la mascota");
        }
    }
}
