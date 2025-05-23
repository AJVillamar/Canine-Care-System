using Domain.ModuleClient.Pets.Models;
using Domain.ModuleClient.Services.Models;
using Domain.ModuleClient.Appointments.Models;
using Domain.ModuleAdministration.Professionals.Models;

namespace Domain.ModuleClient.Appointments.Builders
{
    public class AppointmentBuilder
    {
        private Guid? _id;
        private Pet? _pet;
        private ServiceDetail _service;
        private DateOnly _date;
        private TimeOnly _time;
        private Professional? _professional;
        private string? _reason;
        private string? _status;

        public AppointmentBuilder() { }

        public AppointmentBuilder WithId(Guid id) => SetProperty(ref _id, id);
        public AppointmentBuilder WithPet(Pet pet) => SetProperty(ref _pet, pet);
        public AppointmentBuilder WithDate(DateOnly date) => SetProperty(ref _date, date);
        public AppointmentBuilder WithTime(TimeOnly time) => SetProperty(ref _time, time);
        public AppointmentBuilder WithReason(string? reason) => SetProperty(ref _reason, reason);
        public AppointmentBuilder WithStatus(string status) => SetProperty(ref _status, status);
        public AppointmentBuilder WithService(ServiceDetail service) => SetProperty(ref _service, service);
        public AppointmentBuilder WithProfessional(Professional professional) => SetProperty(ref _professional, professional);

        public Appointment Build()
        {
            return Appointment.Create(
                _id,
                _pet,
                _service,
                _date,
                _time,
                _professional!,
                _reason,
                _status
            );
        }

        public Appointment BuildMinimal()
        {
            return Appointment.Create(_id!.Value);
        }

        private AppointmentBuilder SetProperty<T>(ref T field, T value)
        {
            field = value;
            return this;
        }
    }
}
