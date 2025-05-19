using Domain.Shared.Exceptions;
using Domain.ModuleIdentity.Users.Repositories;
using Application.ModuleIdentity.Authentication.Commands.Login;

namespace Application.ModuleIdentity.Authentication.Validators
{
    public class AuthenticationValidator
    {
        private readonly IUserRepository _userRepository;

        public AuthenticationValidator(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task ValidateLoginAsync(LoginCommand command)
        {
            if (await _userRepository.GetByIdentificationAsync(command.Identification!) == null)
                throw new AuthenticationFailedException("El número de identificación no está registrado.");
        }
    }
}
