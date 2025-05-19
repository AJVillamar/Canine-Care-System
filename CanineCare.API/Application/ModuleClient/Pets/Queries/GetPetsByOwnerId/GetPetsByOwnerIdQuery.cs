using Application.ModuleClient.Pets.Dtos;
using MediatR;
using Shared.Responses;

namespace Application.ModuleClient.Pets.Queries.GetPetsByOwnerId
{
    public class GetPetsByOwnerIdQuery : IRequest<ApiResult<List<PetDto>>>
    {
        public Guid OwnerId { get; set; }
    }
}
