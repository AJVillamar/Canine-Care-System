using Domain.Shared.Repositories;
using Domain.ModuleClient.Appointments.Models;

namespace Domain.ModuleClient.Appointments.Repositories
{
    public interface IAppointmentRepository : IRepository<Appointment> 
    {
        Task CancelAsync(Appointment entity);

        Task<List<TimeOnly>> GetAppointmentHoursAsync();

        Task<List<Appointment>> GetByDateAsync(DateOnly date);

        Task<List<Appointment>> SearchAsync(AppointmentSearchFilter filter);

        Task<List<Appointment>> GetByWeekAsync(DateOnly anyDateInWeek);
    }
}
