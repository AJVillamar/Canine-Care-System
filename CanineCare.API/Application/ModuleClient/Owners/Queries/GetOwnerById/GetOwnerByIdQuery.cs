using MediatR;
using Shared.Responses;
using Application.ModuleClient.Owners.Dtos;

namespace Application.ModuleClient.Owners.Queries.GetOwnerById
{
    public class GetOwnerByIdQuery : IRequest<ApiResult<OwnerDto>>
    {
        public Guid Id { get; set; }
    }
}
