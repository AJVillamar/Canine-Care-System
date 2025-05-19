using Domain.ModuleIdentity.Roles.Enums;
using Domain.ModuleIdentity.Roles.Models;

namespace Application.ModuleIdentity.Roles.Commands.CreateDefaultRoles
{
    public class DefaultRolesProvider
    {
        public static List<Role> GetDefaultRoles()
        {
            return Enum.GetValues(typeof(RoleType))
                .Cast<RoleType>()
                .Select(type => new Role(type.GetSpanishValue()))
                .ToList();
        }
    }
}
