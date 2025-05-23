using MediatR;
using Shared.Responses;
using Application.ModuleClient.Owners.Dtos;

namespace Application.ModuleClient.Owners.Queries.GetOwnerByIdentification
{
    public class GetOwnerByIdentificationQuery : IRequest<ApiResult<OwnerDto>>
    {
        public string? Identification { get; set; }
    }
}
