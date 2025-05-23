using MediatR;
using Shared.Responses;
using Application.ModuleClient.Pets.Dtos;

namespace Application.ModuleClient.Pets.Queries.GetPetsByOwnerIdentification
{
    public class GetPetsByOwnerIdentificationQuery : IRequest<ApiResult<List<PetDto>>>
    {
        public string? Identification { get; set; }
    }
}
