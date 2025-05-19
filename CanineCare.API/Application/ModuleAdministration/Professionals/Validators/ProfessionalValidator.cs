using Application.ModuleIdentity.Users.Validators;
using Application.ModuleAdministration.Professionals.Commands.CreateProfessional;

namespace Application.ModuleAdministration.Professionals.Validators
{
    public class ProfessionalValidator
    {
        private readonly UserValidator _userValidator;

        public ProfessionalValidator(UserValidator userValidator)
        {
            _userValidator = userValidator;
        }

        public async Task ValidateCreateAsync(CreateProfessionalCommand command)
        {
            await _userValidator.ValidateCreateAsync(
                identification: command.Identification,
                email: command.Email);
        }
    }
}
