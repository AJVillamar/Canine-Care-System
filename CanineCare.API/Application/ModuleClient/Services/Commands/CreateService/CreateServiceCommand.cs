using MediatR;
using Shared.Responses;
using Application.ModuleClient.Services.Dtos;

namespace Application.ModuleClient.Services.Commands.CreateService
{
    public class CreateServiceCommand : IRequest<ApiResult> 
    {
        public string? Name { get; set; }

        public string? Description { get; set; }

        public string? Type { get; set; }

        public List<ServiceActionDto>? Actions { get; set; }
    }
}
