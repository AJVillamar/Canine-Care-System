using MediatR;
using Shared.Responses;
using Application.ModuleClient.Pets.Dtos;

namespace Application.ModuleClient.Pets.Queries.SearchPetsByNameOrOwnerName
{
    public class SearchPetsByNameOrOwnerNameQuery : IRequest<ApiResult<List<PetDto>>>
    {
        public string SearchTerm { get; set; }
    }
}
