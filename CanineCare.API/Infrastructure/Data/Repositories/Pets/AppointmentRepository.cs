using Domain.ModuleAdministration.Professionals.Builders;
using Domain.ModuleClient.Appointments.Enums;
using Domain.ModuleClient.Appointments.Models;
using Domain.ModuleClient.Appointments.Repositories;
using Infrastructure.Data.Context;
using Infrastructure.Data.Mappers.Pets;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Infrastructure.Data.Repositories.Pets
{
    public class AppointmentRepository : IAppointmentRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly AppointmentMapperInfra _mapper;

        public AppointmentRepository(
            ApplicationDbContext context,
            AppointmentMapperInfra mapper )
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task AddAsync(Appointment entity)
        {
            var existingProfessional = await _context.Professionals
                .Where(p => (p.Id == entity.Professional.Id || p.PersonId == entity.Professional.Id ) && p.IsActive)
                .FirstOrDefaultAsync();

            if ( existingProfessional != null)
            {
                var appoinmentEntity = _mapper.ToEntity(entity);
                appoinmentEntity.ProfessionalId = existingProfessional.Id;
                await _context.AddAsync(appoinmentEntity);
                await _context.SaveChangesAsync();
            }
        }

        public async Task UpdateAsync(Appointment entity)
        {
            var existingAppoinment = await _context.Appointments
                .Where(a => a.Id == entity.Id && a.IsActive)
                .FirstOrDefaultAsync();

            var existingProfessional = await _context.Professionals
                .Where(p => (p.Id == entity.Professional.Id || p.PersonId == entity.Professional.Id) && p.IsActive)
                .FirstOrDefaultAsync();

            if (existingAppoinment != null && existingProfessional != null)
            {
                var professional = new ProfessionalBuilder()
                    .WithId(existingProfessional.Id)
                    .BuildMinimal();

                entity.UpdateProfessional(professional);

                _mapper.ToEntity(existingAppoinment, entity);
                await _context.SaveChangesAsync();
            }
        }

        public async Task CancelAsync(Appointment entity)
        {
            var existingAppoinment = await _context.Appointments
                .Where(a => a.Id == entity.Id && a.IsActive)
                .FirstOrDefaultAsync();

            if (existingAppoinment != null)
            {
                _mapper.ToEntityCancel(existingAppoinment, entity);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Appointment?> GetByIdAsync(Guid id)
        {
            return await _context.Appointments
                .Include(a => a.Pet)
                .Include(a => a.Service)
                    .ThenInclude(a => a.ServiceDetails)
                .Include(a => a.Professional)
                    .ThenInclude(a => a.Person)
                .Where(a => a.Id == id && a.IsActive)
                .Select(a => _mapper.ToDomain(a))
                .FirstOrDefaultAsync();
        }

        public Task<List<TimeOnly>> GetAppointmentHoursAsync()
        {
            var allHours = Enum.GetValues<AppointmentHour>()
                .Select(h => h.ToTimeOnly())
                .ToList();

            return Task.FromResult(allHours);
        }

        public async Task<List<Appointment>> GetByDateAsync(DateOnly date)
        {
            return await _context.Appointments
                .Include(a => a.Pet)
                .Include(a => a.Service)
                    .ThenInclude(a => a.ServiceDetails)
                .Include(a => a.Professional)
                    .ThenInclude(a => a.Person)
                .Where(a => a.Date == date && a.IsActive)
                .Select(a => _mapper.ToDomain(a))
                .ToListAsync();
        }

        public async Task<List<Appointment>> GetByWeekAsync(DateOnly anyDateInWeek)
        {
            var dayOfWeek = (int)anyDateInWeek.DayOfWeek;
            var offset = dayOfWeek == 0 ? -6 : -dayOfWeek + 1;
            var startOfWeek = anyDateInWeek.AddDays(offset);
            var endOfWeek = startOfWeek.AddDays(6);

            return await _context.Appointments
                .Include(a => a.Pet)
                .Include(a => a.Service)
                    .ThenInclude(a => a.ServiceDetails)
                .Include(a => a.Professional)
                    .ThenInclude(a => a.Person)
                .Where(a => (a.Date >= startOfWeek && a.Date <= endOfWeek) && a.IsActive)
                .Select(a => _mapper.ToDomain(a))
                .ToListAsync();
        }

        public async Task<IEnumerable<Appointment>> GetAllAsync()
        {
            return await _context.Appointments
                .Include(a => a.Pet)
                .Include(a => a.Service)
                    .ThenInclude(a => a.ServiceDetails)
                .Include(a => a.Professional)
                    .ThenInclude(a => a.Person)
                .OrderBy(p => p.Date)
                .Select(a => _mapper.ToDomain(a))
                .ToListAsync();
        }

        public async Task<List<Appointment>> SearchAsync(AppointmentSearchFilter filter)
        {
            var text = filter.Query?.Trim().ToLower();

            return await _context.Appointments
                .Include(a => a.Pet)
                .Include(a => a.Service)
                    .ThenInclude(a => a.ServiceDetails)
                .Include(a => a.Professional)
                    .ThenInclude(a => a.Person)
                .Where(a =>
                    a.IsActive &&
                    (
                        (filter.Date.HasValue && a.Date == filter.Date.Value) ||
                        (!string.IsNullOrEmpty(text) && (
                            a.Pet.Name.ToLower().Contains(text) ||
                            a.Professional.Person.Identification.ToLower().Contains(text) ||
                            a.Professional.Person.FirstName.ToLower().Contains(text) ||
                            a.Professional.Person.LastName.ToLower().Contains(text)
                        ))))
                .OrderBy(p => p.Date)
                .Select(a => _mapper.ToDomain(a))
                .ToListAsync();
        }
    }
}
