namespace Domain.ModuleClient.Appointments.Models
{
    public class AppointmentSearchFilter
    {
        public DateOnly? Date { get; private set; }

        public string? Query { get; private set; }

        private AppointmentSearchFilter(DateOnly? date, string? query)
        {
            Date = date;
            Query = string.IsNullOrWhiteSpace(query) ? null : query.Trim();
        }

        public static AppointmentSearchFilter Create(DateOnly? date = null, string? query = null)
        {
            return new AppointmentSearchFilter(date, query);
        }
    }
}
