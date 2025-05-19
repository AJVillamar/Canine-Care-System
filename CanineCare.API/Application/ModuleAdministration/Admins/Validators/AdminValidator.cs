using Domain.Shared.Exceptions;
using Domain.ModuleAdministration.Admins.Models;
using Application.ModuleIdentity.Users.Validators;
using Domain.ModuleAdministration.Admins.Repositories;

namespace Application.ModuleAdministration.Admins.Validators
{
    public class AdminValidator
    {
        private readonly IAdminRepository _adminRepository;
        private readonly UserValidator _userValidator;

        public AdminValidator(
            IAdminRepository adminRepository,
            UserValidator userValidator)
        {
            _adminRepository = adminRepository;
            _userValidator = userValidator;
        }

        public async Task ValidateCreateAsync(Admin command)
        {
            if (await _adminRepository.ExistsAdminRoleAsync())
                throw new BusinessRuleViolationException("Lo sentimos ya hay un administrador creado");

            await _userValidator.ValidateCreateAsync(command.Identification.Value, command.Email.Value);
        }
    }
}
