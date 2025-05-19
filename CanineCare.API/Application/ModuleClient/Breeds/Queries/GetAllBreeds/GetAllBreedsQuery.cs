using MediatR;
using Shared.Responses;
using Application.ModuleClient.Breeds.Dtos;

namespace Application.ModuleClient.Breeds.Queries.GetAllBreeds
{
    public class GetAllBreedsQuery : IRequest<ApiResult<List<BreedDto>>> { }
}
