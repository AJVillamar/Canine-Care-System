using MediatR;
using Shared.Responses;
using Application.ModuleClient.Services.Dtos;

namespace Application.ModuleClient.Services.Queries.GetServiceById
{
    public class GetServiceByIdQuery : IRequest<ApiResult<ServiceDto>>
    {
        public Guid Id { get; set; }
    }
}
