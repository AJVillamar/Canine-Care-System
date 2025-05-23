using Domain.Shared.Exceptions;
using Domain.ModuleClient.Appointments.Enums;
using Application.ModuleClient.Pets.Validators;
using Application.ModuleClient.Services.Validator;
using Domain.ModuleClient.Appointments.Repositories;
using Application.ModuleAdministration.Professionals.Validators;
using Application.ModuleClient.Appointments.Commands.CreateAppointment;
using Application.ModuleClient.Appointments.Commands.UpdateAppointment;

namespace Application.ModuleClient.Appointments.Validators
{
    public class AppointmentValidator
    {
        private readonly IAppointmentRepository _repository;
        private readonly PetValidator _petValidator;
        private readonly ProfessionalValidator _professionalValidator;
        private readonly ServiceValidator _serviceValidator;

        public AppointmentValidator(
            IAppointmentRepository repository,
            PetValidator petValidator,
            ProfessionalValidator professionalValidator,
            ServiceValidator serviceValidator )
        {
            _repository = repository;
            _petValidator = petValidator;
            _professionalValidator = professionalValidator;
            _serviceValidator = serviceValidator;
        }

        public async Task ValidateCreateAsync(CreateAppointmentCommand command)
        {
            await _petValidator.ValidateGetByIdAsync(command.PetId);
            await _professionalValidator.ValidateGetByIdAsync(command.ProfessionalId);
            await _serviceValidator.ValidateGetByIdAsync(command.ServiceId);

            var appointments = await _repository.GetByDateAsync(command.Date);
            var hour = AppointmentHourExtensions.ParseFromSpanishName(command.Time);

            if (appointments.Any(a => a.Professional.Id == command.ProfessionalId && a.Time == hour))
                throw new BusinessRuleViolationException("El profesional ya tiene una cita a esa hora.");

            if (appointments.Any(a => a.Pet.Id == command.PetId))
                throw new BusinessRuleViolationException("La mascota ya tiene una cita ese día.");
        }

        public async Task ValidateUpdateAsync(UpdateAppointmentCommand command)
        {
            var existing = await _repository.GetByIdAsync(command.Id);
            if (existing == null)
                throw new NotFoundException("Cita");

            if (existing.Status == AppointmentStatus.Cancelled || existing.Status == AppointmentStatus.Completed)
                throw new BusinessRuleViolationException("No se puede modificar una cita cancelada o ya realizada.");

            await _professionalValidator.ValidateGetByIdAsync(command.ProfessionalId);
            await _serviceValidator.ValidateGetByIdAsync(command.ServiceId);

            var appointments = await _repository.GetByDateAsync(command.Date);
            var parsedHour = AppointmentHourExtensions.ParseFromSpanishName(command.Time);

            if (appointments.Any(a => a.Id != command.Id && a.Professional.Id == command.ProfessionalId && a.Time == parsedHour))
                throw new BusinessRuleViolationException("El profesional ya tiene una cita en ese horario.");

            if (appointments.Any(a => a.Id != command.Id && a.Pet.Id == existing.Pet.Id))
                throw new BusinessRuleViolationException("La mascota ya tiene una cita agendada ese día.");
        }

        public async Task ValidateGetByIdAsync(Guid id)
        {
            if (await _repository.GetByIdAsync(id) == null)
                throw new NotFoundException("Cita");
        }
    }
}
