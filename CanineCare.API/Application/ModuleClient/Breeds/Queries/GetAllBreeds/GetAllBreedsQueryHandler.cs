using MediatR;
using Shared.Responses;
using Application.ModuleClient.Breeds.Dtos;
using Application.ModuleClient.Breeds.Mappers;
using Domain.ModuleClient.Pets.Repositories;

namespace Application.ModuleClient.Breeds.Queries.GetAllBreeds
{
    public class GetAllBreedsQueryHandler : IRequestHandler<GetAllBreedsQuery, ApiResult<List<BreedDto>>>
    {
        private readonly IBreedRepository _repository;
        private readonly BreedMapperApp _mapper;

        public GetAllBreedsQueryHandler(
            IBreedRepository repository,
            BreedMapperApp mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ApiResult<List<BreedDto>>> Handle(GetAllBreedsQuery request, CancellationToken cancellationToken)
        {
            var breeds = await _repository.GetAllAsync();
            var breedDtos = breeds.Select(_mapper.ToDto).ToList();

            var message = breedDtos.Any()
                ? "Lista de razas"
                : "Aún no hay razas registradas";

            return new ApiResult<List<BreedDto>>(200, breedDtos, message);
        }
    }
}
