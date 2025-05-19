using Domain.ModuleIdentity.Roles.Models;
using Infrastructure.Data.Entities.Identity;

namespace Infrastructure.Data.Mappers.Identity
{
    public class RoleMapperInfra
    {
        public Role ToDomain(RoleEntity entity)
        {
            return new Role(entity.Id, entity.Name!);
        }

        public RoleEntity ToEntity(Role domain)
        {
            return new RoleEntity
            {
                Id = domain.Id,
                Name = domain.Name,
            };
        }
    }
}
