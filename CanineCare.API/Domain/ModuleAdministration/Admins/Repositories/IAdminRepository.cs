using Domain.ModuleAdministration.Admins.Models;

namespace Domain.ModuleAdministration.Admins.Repositories
{
    public interface IAdminRepository
    {
        Task AddAsync(Admin entity);

        Task<bool> ExistsAdminRoleAsync();
    }
}
