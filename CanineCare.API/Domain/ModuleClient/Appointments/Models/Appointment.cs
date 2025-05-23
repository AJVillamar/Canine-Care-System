using Domain.Shared.Exceptions;
using Domain.ModuleClient.Pets.Models;
using Domain.ModuleClient.Services.Models;
using Domain.ModuleClient.Appointments.Enums;
using Domain.ModuleAdministration.Professionals.Models;

namespace Domain.ModuleClient.Appointments.Models
{
    public class Appointment
    {
        public Guid Id { get; private set; }

        public ServiceDetail Service { get; private set; }

        public Pet Pet { get; private set; }

        public DateOnly Date { get; private set; }

        public AppointmentHour Time { get; private set; }

        public Professional Professional { get; private set; }

        public string? Reason { get; private set; }

        public AppointmentStatus Status { get; private set; }

        private Appointment(Guid id)
        {
            if (id == Guid.Empty)
                throw new EmptyFieldException("Id de la cita");

            Id = id;
        }

        private Appointment(
            Guid id, Pet pet, ServiceDetail service, DateOnly date, AppointmentHour time, 
            Professional professional, string? reason, AppointmentStatus status )
        {
            Id = id;
            Pet = pet;
            Service = service;
            Date = date;
            Time = time;
            Professional = professional;
            Reason = string.IsNullOrWhiteSpace(reason) ? null : reason.Trim();
            Status = status;

            Validate();
        }

        public static Appointment Create(
            Guid? id, Pet pet, ServiceDetail service, DateOnly date, TimeOnly time, Professional professional, 
            string? reason = null, string? status = null)
        {
            var resolvedStatus = id == null ? AppointmentStatus.Pending : AppointmentStatusExtensions.ParseFromSpanishName(status);
            var parsedHour = AppointmentHourExtensions.ParseFromSpanishName(time);

            return new Appointment(
                id ?? Guid.NewGuid(),
                pet,
                service,
                date,
                parsedHour,
                professional,
                reason,
                resolvedStatus
            );
        }

        public static Appointment Create(Guid id)
        {
            return new Appointment(id);
        }

        public void UpdateService(ServiceDetail service)
        {
            Service = service;
            Validate();
        }

        public void UpdateDate(DateOnly newDate)
        {
            Date = newDate;
            Validate();
        }

        public void UpdateTime(TimeOnly newTime)
        {
            Time = AppointmentHourExtensions.ParseFromSpanishName(newTime);
            Validate();
        }

        public void UpdateProfessional(Professional newProfessional)
        {
            Professional = newProfessional ?? throw new BusinessRuleViolationException("El profesional no puede ser nulo.");
            Validate();
        }

        public void UpdateReason(string? newReason)
        {
            Reason = string.IsNullOrWhiteSpace(newReason) ? null : newReason.Trim();
        }

        public void Cancel()
        {
            if (Status == AppointmentStatus.Cancelled)
                throw new BusinessRuleViolationException("La cita ya está cancelada.");

            if (Status == AppointmentStatus.Completed)
                throw new BusinessRuleViolationException("No se puede cancelar una cita que ya fue realizada.");

            Status = AppointmentStatus.Cancelled;
        }

        private void Validate()
        {
            if (Pet is null)
                throw new BusinessRuleViolationException("La cita debe estar asociada a una mascota.");

            if (Service is null)
                throw new BusinessRuleViolationException("La cita debe estar asociada a un servicio.");

            if (Professional is null)
                throw new BusinessRuleViolationException("La cita debe tener un profesional asignado.");

            if (Date < DateOnly.FromDateTime(DateTime.Today))
                throw new BusinessRuleViolationException("La fecha de la cita no puede ser anterior al día actual.");
        }
    }
}
