using Domain.Shared.Repositories;
using Domain.ModuleClient.Pets.Models;

namespace Domain.ModuleClient.Pets.Repositories
{
    public interface IPetRepository : IRepository<Pet> 
    {
        Task<IEnumerable<Pet>> SearchByPetNameOrOwnerNameAsync(string searchTerm);

        Task<IEnumerable<Pet>> GetByOwnerIdAsync(Guid ownerId);

        Task<IEnumerable<Pet>> GetByOwnerIdentificationAsync(string identification);
    }
}
