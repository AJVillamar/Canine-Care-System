using Domain.Shared.Exceptions;

namespace Domain.ModuleClient.Appointments.Enums
{
    public enum AppointmentStatus
    {
        Pending,
        Confirmed,
        Completed,
        Cancelled
    }

    public static class AppointmentStatusExtensions
    {
        private static readonly Dictionary<AppointmentStatus, string> _spanish = new()
        {
            { AppointmentStatus.Pending, "Pendiente" },
            { AppointmentStatus.Confirmed, "Confirmada" },
            { AppointmentStatus.Completed, "Realizada" },
            { AppointmentStatus.Cancelled, "Cancelada" },
        };

        public static string GetSpanishValue(this AppointmentStatus status) =>
            _spanish.TryGetValue(status, out var value) ? value : "Desconocido";

        public static AppointmentStatus ParseFromSpanishName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new EmptyFieldException("Estado");

            var normalizedInput = name.Trim().ToLowerInvariant();

            foreach (var kvp in _spanish)
            {
                if (kvp.Value.ToLowerInvariant() == normalizedInput)
                    return kvp.Key;
            }

            throw new BusinessRuleViolationException($"Estado '{name}' no es válido.");
        }
    }
}
