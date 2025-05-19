using MediatR;
using Shared.Responses;
using Application.ModuleClient.Pets.Dtos;

namespace Application.ModuleClient.Pets.Queries.GetAllPets
{
    public class GetAllPetsQuery : IRequest<ApiResult<List<PetDto>>> { }
}
