using Domain.ModuleIdentity.Authentication.Models;

namespace Domain.ModuleIdentity.Authentication.Repositories
{
    public interface IAuthenticationRepository
    {
        Task<string> AuthenticateAsync(Credentials entity);
    }
}
