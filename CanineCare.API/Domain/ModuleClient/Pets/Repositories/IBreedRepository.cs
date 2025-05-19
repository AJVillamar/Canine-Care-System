using Domain.Shared.Repositories;
using Domain.ModuleClient.Pets.Models;

namespace Domain.ModuleClient.Pets.Repositories
{
    public interface IBreedRepository : ISimpleRepository<Breed> { }
}
