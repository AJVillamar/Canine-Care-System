using Domain.ModuleIdentity.Users.Models;

namespace Domain.ModuleIdentity.Users.Repositories
{
    public interface IUserRepository
    {
        Task<User?> GetByIdAsync(Guid id);

        Task<User?> GetByIdentificationAsync(string identification);

        Task<User?> GetByEmailAsync(string email);
    }
}
