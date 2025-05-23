using Application.ModuleAdministration.Professionals.Commands.CreateProfessional;
using Application.ModuleIdentity.Users.Validators;
using Domain.ModuleAdministration.Professionals.Repositories;
using Domain.ModuleClient.Pets.Repositories;
using Domain.Shared.Exceptions;

namespace Application.ModuleAdministration.Professionals.Validators
{
    public class ProfessionalValidator
    {
        private readonly UserValidator _userValidator;
        private readonly IProfessionalRepository _professionalRepository;

        public ProfessionalValidator(
            UserValidator userValidator,
            IProfessionalRepository professionalRepository )
        {
            _userValidator = userValidator;
            _professionalRepository = professionalRepository;
        }

        public async Task ValidateCreateAsync(CreateProfessionalCommand command)
        {
            await _userValidator.ValidateCreateAsync(
                identification: command.Identification,
                email: command.Email);
        }

        public async Task ValidateGetByIdAsync(Guid id)
        {
            if (await _professionalRepository.GetByIdAsync(id) == null)
                throw new NotFoundException("Profesional");
        }
    }
}
