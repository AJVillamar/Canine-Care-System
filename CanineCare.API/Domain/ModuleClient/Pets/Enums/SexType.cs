using Domain.Shared.Exceptions;

namespace Domain.ModuleClient.Pets.Enums
{
    public enum SexType
    {
        Male,
        Female
    }

    public static class SexTypeExtensions
    {
        private static readonly Dictionary<SexType, string> _sexNames = new()
        {
            { SexType.Male, "Macho" },
            { SexType.Female, "Hembra" }
        };

        public static string GetSpanishValue(this SexType sexType) =>
            _sexNames.TryGetValue(sexType, out var name) ? name : "Desconocido";


        public static SexType ParseFromSpanishName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new EmptyFieldException("Sexo");

            var normalizedInput = name.Trim().ToLowerInvariant();

            foreach (var kvp in _sexNames)
            {
                if (kvp.Value.ToLowerInvariant() == normalizedInput)
                    return kvp.Key;
            }

            throw new BusinessRuleViolationException($"Sexo '{name}' no es válido.");
        }
    }
}
