using Application.ModuleClient.Owners.Dtos;
using MediatR;
using Shared.Responses;

namespace Application.ModuleClient.Owners.Queries.GetAllOwners
{
    public class GetAllOwnersQuery : IRequest<ApiResult<List<OwnerDto>>> { }
}
