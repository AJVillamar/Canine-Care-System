using Infrastructure.Data.Entities.Pet;
using Domain.ModuleClient.Services.Enums;
using Domain.ModuleClient.Services.Models;
using Domain.ModuleClient.Services.Builders;

namespace Infrastructure.Data.Mappers.Pets
{
    public class ServiceMaperInfra
    {
        public ServiceDetail ToDomain(ServiceEntity entity) 
        {
            var type = ServiceTypeExtensions.ParseFromSpanishName(entity.Type);

            var actions = entity.ServiceDetails.Select(item => ServiceActionDetail.Create(item.ActionName, item.ActionDescription)).ToList();

            return new ServiceDetailBuilder()
                .WithId(entity.Id)
                .WithName(entity.Name)
                .WithDescription(entity.Description)
                .WithType(entity.Type)
                .WithActions(actions)
                .Build();
        }

        public ServiceEntity ToEntity(ServiceDetail domain)
        {
            return new ServiceEntity
            {
                Id = domain.Id,
                Name = domain.Name,
                Description = domain.Description,
                Type = ServiceTypeExtensions.GetSpanishValue(domain.Type)
            };
        }

        public ServiceDetailEntity ToEntityDetail(string detailName, string detailDescription, Guid serviceId)
        {
            return new ServiceDetailEntity
            {
                Id = Guid.NewGuid(),
                ServiceId = serviceId,
                ActionName = detailName,
                ActionDescription = detailDescription,
            };
        }
    }
}
