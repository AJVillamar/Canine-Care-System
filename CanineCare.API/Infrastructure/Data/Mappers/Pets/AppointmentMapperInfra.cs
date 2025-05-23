using Infrastructure.Data.Entities.Pet;
using Infrastructure.Data.Mappers.People;
using Domain.ModuleClient.Appointments.Enums;
using Domain.ModuleClient.Appointments.Models;
using Domain.ModuleClient.Appointments.Builders;

namespace Infrastructure.Data.Mappers.Pets
{
    public class AppointmentMapperInfra
    {
        private readonly PetMapperInfra _petMapper;
        private readonly ServiceMaperInfra _serviceMaper;
        private readonly ProfessionalMapperInfra _professionalMapper;

        public AppointmentMapperInfra(
            PetMapperInfra petMapper,
            ServiceMaperInfra serviceMaper, 
            ProfessionalMapperInfra professionalMapper )
        {
            _petMapper = petMapper;
            _serviceMaper = serviceMaper;
            _professionalMapper = professionalMapper;
        }

        public Appointment ToDomain(AppointmentEntity entity)
        {
            var pet = _petMapper.ToDomain(entity.Pet.Id, entity.Pet.Name, entity.Pet.OwnerId);
            var service = _serviceMaper.ToDomain(entity.Service);
            var professional = _professionalMapper.ToDomain(entity.Professional);

            return new AppointmentBuilder()
                .WithId(entity.Id)
                .WithPet(pet)
                .WithService(service)
                .WithDate(entity.Date)
                .WithTime(entity.Time)
                .WithProfessional(professional)
                .WithReason(entity.Reason)     
                .WithStatus(entity.Status)
                .Build();
        }

        public AppointmentEntity ToEntity(Appointment domain)
        {
            return new AppointmentEntity
            {
                Id = domain.Id,
                PetId = domain.Pet.Id,
                ServiceId = domain.Service.Id,
                Date = domain.Date,
                Time = domain.Time.ToTimeOnly(),
                ProfessionalId = domain.Professional.Id,
                Reason = domain.Reason != null ? domain.Reason : null,
                Status = domain.Status.GetSpanishValue()
            };
        }

        public AppointmentEntity ToEntity(AppointmentEntity entity, Appointment domain)
        {
            entity.ServiceId = domain.Service.Id;
            entity.Date = domain.Date;
            entity.Time = domain.Time.ToTimeOnly();
            entity.ProfessionalId = domain.Professional.Id;
            entity.UpdatedAt = DateTime.Now;
            return entity;
        }

        public AppointmentEntity ToEntityCancel(AppointmentEntity entity, Appointment domain)
        {
            entity.Status = domain.Status.GetSpanishValue();
            entity.DeletedAt = DateTime.Now;
            return entity;
        }
    }
}
