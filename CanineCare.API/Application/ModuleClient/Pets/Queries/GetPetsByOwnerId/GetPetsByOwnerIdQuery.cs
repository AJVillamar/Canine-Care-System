using MediatR;
using Shared.Responses;
using Application.ModuleClient.Pets.Dtos;

namespace Application.ModuleClient.Pets.Queries.GetPetsByOwnerId
{
    public class GetPetsByOwnerIdQuery : IRequest<ApiResult<List<PetDto>>>
    {
        public Guid OwnerId { get; set; }
    }
}
