using Domain.ModuleClient.Services.Models;
using Domain.Shared.Repositories;

namespace Domain.ModuleClient.Services.Repositories
{
    public interface IServiceRepository : IRepository<ServiceDetail>
    {
        Task<ServiceDetail?> GetByNameAsync(string name);

        Task<List<string>> GetAllServiceTypes();
    }
}
