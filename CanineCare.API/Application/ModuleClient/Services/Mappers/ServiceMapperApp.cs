using Domain.ModuleClient.Services.Enums;
using Domain.ModuleClient.Services.Models;
using Domain.ModuleClient.Services.Builders;
using Application.ModuleClient.Services.Dtos;
using Application.ModuleClient.Services.Commands.CreateService;

namespace Application.ModuleClient.Services.Mappers
{
    public class ServiceMapperApp
    {
        public ServiceDetail ToDomain(Guid id)
        {
            return new ServiceDetailBuilder()
                .WithId(id)
                .BuildMinimal();
        }

        public ServiceDetail ToDomain(CreateServiceCommand command)
        {
            var actions = command.Actions.Select(item => ServiceActionDetail.Create(item.Title, item.Details)).ToList();

            return new ServiceDetailBuilder()
                .WithName(command.Name)
                .WithDescription(command.Description)
                .WithType(command.Type)
                .WithActions(actions)
                .Build();
        }

        public ServiceDto ToDto(ServiceDetail domain)
        {
            var actions = domain.Actions?.Select(m => new ServiceActionDto { Title = m.Title, Details = m.Description }).ToList();

            return new ServiceDto
            {
                Id = domain.Id,
                Name = domain.Name,
                Description = domain.Description,
                Type = domain.Type.GetSpanishValue(),
                Actions = actions
            };
        }
    }
}
