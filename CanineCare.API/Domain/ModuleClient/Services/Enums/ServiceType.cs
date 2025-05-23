using Domain.Shared.Exceptions;

namespace Domain.ModuleClient.Services.Enums
{
    public enum ServiceType
    {
        NormalBath,
        MedicatedBath,
        Grooming,
        Deworming
    }

    public static class ServiceTypeExtensions
    {
        private static readonly Dictionary<ServiceType, string> _typeNames = new()
        {
            { ServiceType.NormalBath, "Baño Normal" },
            { ServiceType.MedicatedBath, "Baño Medicado" },
            { ServiceType.Grooming, "Peluquería Canina" },
            { ServiceType.Deworming, "Desparasitación" }
        };

        public static string GetSpanishValue(this ServiceType type) =>
            _typeNames.TryGetValue(type, out var name) ? name : "Desconocido";

        public static ServiceType ParseFromSpanishName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new EmptyFieldException("tipo de servicio");

            var normalizedInput = name.Trim().ToLowerInvariant();

            foreach (var kvp in _typeNames)
            {
                if (kvp.Value.ToLowerInvariant() == normalizedInput)
                    return kvp.Key;
            }

            throw new BusinessRuleViolationException($"Tipo de servicio '{name}' no es válido.");
        }
    }
}
