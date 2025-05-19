using Domain.ModuleIdentity.Authentication.Models;
using Application.ModuleIdentity.Authentication.Commands.Login;

namespace Application.ModuleIdentity.Authentication.Mappers
{
    public class AuthenticationMapperApp
    {
        public Credentials ToDomainCredentials(LoginCommand command)
        {
            return new Credentials(command.Identification!, command.Password!);
        }
    }
}
