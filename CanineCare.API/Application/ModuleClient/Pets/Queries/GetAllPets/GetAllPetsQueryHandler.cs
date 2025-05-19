using MediatR;
using Shared.Responses;
using Application.ModuleClient.Pets.Dtos;
using Application.ModuleClient.Pets.Mappers;
using Domain.ModuleClient.Pets.Repositories;

namespace Application.ModuleClient.Pets.Queries.GetAllPets
{
    public class GetAllPetsQueryHandler : IRequestHandler<GetAllPetsQuery, ApiResult<List<PetDto>>>
    {
        private readonly IPetRepository _repository;
        private readonly PetMapperApp _mapper;

        public GetAllPetsQueryHandler(
            IPetRepository repository,
            PetMapperApp mapper )
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ApiResult<List<PetDto>>> Handle(GetAllPetsQuery request, CancellationToken cancellationToken)
        {
            var pets = await _repository.GetAllAsync();
            var petDtos = pets.Select(_mapper.ToDto).ToList();

            var message = petDtos.Any()
                ? "Lista de mascotas"
                : "Aún no hay mascotas registradas";

            return new ApiResult<List<PetDto>>(200, petDtos, message);
        }
    }
}
