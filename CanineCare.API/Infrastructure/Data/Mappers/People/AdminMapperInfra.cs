using Infrastructure.Data.Entities.People;
using Domain.ModuleAdministration.Admins.Models;

namespace Infrastructure.Data.Mappers.People
{
    public class AdminMapperInfra
    {
        public AdminEntity ToEntity(Admin admin)
        {
            return new AdminEntity
            {
                Id = Guid.NewGuid(),
                PersonId = admin.Id,
                BirthDate = admin.BirthDate
            };
        }
    }
}
