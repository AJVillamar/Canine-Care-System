namespace Domain.ModuleIdentity.Roles.Enums
{
    public enum RoleType
    {
        Administrator,
        Client,
        Professional
    }

    public static class RoleTypeExtensions
    {
        private static readonly Dictionary<RoleType, string> _roleNames = new()
        {
            { RoleType.Administrator, "Administrador" },
            { RoleType.Client, "Cliente" },
            { RoleType.Professional, "Profesional" }
        };

        public static string GetSpanishValue(this RoleType roleType) =>
            _roleNames.TryGetValue(roleType, out var name) ? name : "Desconocido";
    }
}
