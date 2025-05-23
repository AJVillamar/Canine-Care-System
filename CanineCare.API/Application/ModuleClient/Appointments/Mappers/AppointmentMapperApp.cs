using Application.ModuleAdministration.Professionals.Mappers;
using Application.ModuleClient.Appointments.Commands.CreateAppointment;
using Application.ModuleClient.Appointments.Commands.UpdateAppointment;
using Application.ModuleClient.Appointments.Dtos;
using Application.ModuleClient.Appointments.Queries.SearchAppointments;
using Application.ModuleClient.Pets.Mappers;
using Application.ModuleClient.Services.Mappers;
using Domain.ModuleClient.Appointments.Builders;
using Domain.ModuleClient.Appointments.Enums;
using Domain.ModuleClient.Appointments.Models;

namespace Application.ModuleClient.Appointments.Mappers
{
    public class AppointmentMapperApp
    {
        private readonly PetMapperApp _petMapper;
        private readonly ServiceMapperApp _serviceMapper;
        private readonly ProfessionalMapperApp _professionalMapper;

        public AppointmentMapperApp(
            PetMapperApp petMapper,
            ServiceMapperApp serviceMapper,
            ProfessionalMapperApp professionalMapper )
        {
            _petMapper = petMapper;
            _serviceMapper = serviceMapper;
            _professionalMapper = professionalMapper;
        }

        public Appointment ToDomain(CreateAppointmentCommand command)
        {
            return new AppointmentBuilder()
                .WithPet(_petMapper.ToDomain(command.PetId))
                .WithService(_serviceMapper.ToDomain(command.ServiceId))
                .WithDate(command.Date)
                .WithTime(command.Time)
                .WithProfessional(_professionalMapper.ToDomain(command.ProfessionalId))
                .WithReason(command.Reason)
                .Build();
        }

        public Appointment ToDomain(UpdateAppointmentCommand command, Appointment domain)
        {            
            domain.UpdateService(_serviceMapper.ToDomain(command.ServiceId));
            domain.UpdateDate(command.Date);
            domain.UpdateTime(command.Time);
            domain.UpdateProfessional(_professionalMapper.ToDomain(command.ProfessionalId));
            return domain;
        }

        public AppointmentSearchFilter ToFilter(SearchAppointmentsQuery query)
        {
            return AppointmentSearchFilter.Create(
                query.Date, 
                query.Query
            );
        }

        public AppointmentDto ToDto(Appointment domain)
        {
            return new AppointmentDto
            {
                Id = domain.Id,
                Pet = _petMapper.ToDto(domain.Pet.Id, domain.Pet.Name, domain.Pet.Owner.Id),
                Service = _serviceMapper.ToDto(domain.Service),
                Date = domain.Date,
                Time = domain.Time.ToTimeOnly(),
                Professional = _professionalMapper.ToDto(domain.Professional),
                Reason = domain.Reason,
                Status = domain.Status.GetSpanishValue()
            };
        }
    }
}
