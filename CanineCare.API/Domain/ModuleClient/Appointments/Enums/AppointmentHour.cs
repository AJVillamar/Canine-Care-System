using Domain.Shared.Exceptions;

namespace Domain.ModuleClient.Appointments.Enums
{
    public enum AppointmentHour
    {
        Hour08,
        Hour09,
        Hour10,
        Hour11,
        Hour12,
        Hour13,
        Hour14,
        Hour15,
        Hour16,
        Hour17
    }

    public static class AppointmentHourExtensions
    {
        private static readonly Dictionary<AppointmentHour, TimeOnly> _hourMapping = new()
        {
            { AppointmentHour.Hour08, new TimeOnly(8, 0) },
            { AppointmentHour.Hour09, new TimeOnly(9, 0) },
            { AppointmentHour.Hour10, new TimeOnly(10, 0) },
            { AppointmentHour.Hour11, new TimeOnly(11, 0) },
            { AppointmentHour.Hour12, new TimeOnly(12, 0) },
            { AppointmentHour.Hour13, new TimeOnly(13, 0) },
            { AppointmentHour.Hour14, new TimeOnly(14, 0) },
            { AppointmentHour.Hour15, new TimeOnly(15, 0) },
            { AppointmentHour.Hour16, new TimeOnly(16, 0) },
            { AppointmentHour.Hour17, new TimeOnly(17, 0) }
        };

        public static TimeOnly ToTimeOnly(this AppointmentHour hour) =>
            _hourMapping[hour];
        
        public static string GetSpanishValue(this AppointmentHour hour) =>
            _hourMapping.TryGetValue(hour, out var value) ? value.ToString("HH:mm") : "Desconocido";

        public static AppointmentHour ParseFromSpanishName(TimeOnly time)
        {
            foreach (var kvp in _hourMapping)
            {
                if (kvp.Value == time)
                    return kvp.Key;
            }

            throw new BusinessRuleViolationException($"Hora '{time}' no válida para agendar.");
        }
    }
}
