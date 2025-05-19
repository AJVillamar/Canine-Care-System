using Domain.Shared.Repositories;
using Domain.ModuleClient.Canines.Models;

namespace Domain.ModuleClient.Pets.Repositories
{
    public interface IPetRepository : IRepository<Pet> 
    {
        Task<IEnumerable<Pet>> SearchByPetNameOrOwnerNameAsync(string searchTerm);

        Task<IEnumerable<Pet>> GetByOwnerIdAsync(Guid ownerId);
    }
}
