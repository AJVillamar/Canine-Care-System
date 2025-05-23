using Domain.Shared.Exceptions;
using Domain.ModuleIdentity.Users.Repositories;

namespace Application.ModuleIdentity.Users.Validators
{
    public class UserValidator
    {
        private readonly IUserRepository _repository;

        public UserValidator(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task ValidateCreateAsync(string? identification = null, string? email = null)
        {
            if (identification != null && await _repository.GetByIdentificationAsync(identification) != null)
                throw new UniqueConstraintViolationException("Cédula de identidad");

            if (!string.IsNullOrWhiteSpace(email) && await _repository.GetByEmailAsync(email) != null)
                throw new UniqueConstraintViolationException("Correo Electrónico");
        }

        public async Task ValidateUpdateAsync(string? email = null)
        {
            if (string.IsNullOrWhiteSpace(email) && await _repository.GetByEmailAsync(email) != null)
                throw new UniqueConstraintViolationException("Correo Electrónico");
        }
    }
}
