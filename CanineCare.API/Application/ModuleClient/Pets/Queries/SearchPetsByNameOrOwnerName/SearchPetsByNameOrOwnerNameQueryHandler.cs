using MediatR;
using Shared.Responses;
using Application.ModuleClient.Pets.Dtos;
using Application.ModuleClient.Pets.Mappers;
using Domain.ModuleClient.Pets.Repositories;

namespace Application.ModuleClient.Pets.Queries.SearchPetsByNameOrOwnerName
{
    public class SearchPetsByNameOrOwnerNameQueryHandler : IRequestHandler<SearchPetsByNameOrOwnerNameQuery, ApiResult<List<PetDto>>>
    {
        private readonly IPetRepository _repository;
        private readonly PetMapperApp _mapper;

        public SearchPetsByNameOrOwnerNameQueryHandler(IPetRepository repository, PetMapperApp mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ApiResult<List<PetDto>>> Handle(SearchPetsByNameOrOwnerNameQuery request, CancellationToken cancellationToken)
        {
            var pets = await _repository.SearchByPetNameOrOwnerNameAsync(request.SearchTerm);
            var petDtos = pets.Select(_mapper.ToDto).ToList();

            var message = petDtos.Any()
                ? "Resultados de la búsqueda"
                : "No se encontraron coincidencias para la búsqueda.";

            return new ApiResult<List<PetDto>>(200, petDtos, message);
        }
    }
}
