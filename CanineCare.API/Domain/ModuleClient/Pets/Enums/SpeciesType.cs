using Domain.Shared.Exceptions;

namespace Domain.ModuleClient.Pets.Enums
{
    public enum SpeciesType
    {
        Canine,
        Feline,
        Avian
    }

    public static class SpeciesTypeExtensions
    {
        private static readonly Dictionary<SpeciesType, string> _speciesNames = new()
        {
            { SpeciesType.Canine, "Canino" },
            { SpeciesType.Feline, "Felino" },
            { SpeciesType.Avian, "Ave" },
        };

        public static string GetSpanishValue(this SpeciesType speciesType) =>
            _speciesNames.TryGetValue(speciesType, out var name) ? name : "Desconocido";

        public static SpeciesType ParseFromSpanishName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new EmptyFieldException("Especie");

            var normalizedInput = name.Trim().ToLowerInvariant();

            foreach (var kvp in _speciesNames)
            {
                if (kvp.Value.ToLowerInvariant() == normalizedInput)
                    return kvp.Key;
            }

            throw new BusinessRuleViolationException($"Especie '{name}' no es válida.");
        }
    }
}
