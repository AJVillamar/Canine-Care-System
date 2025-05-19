using MediatR;
using Shared.Responses;
using Application.ModuleClient.Owners.Dtos;
using Application.ModuleClient.Owners.Mappers;
using Domain.ModuleClient.Owners.Repositories;

namespace Application.ModuleClient.Owners.Queries.GetAllOwners
{
    public class GetAllOwnersQueryHandler : IRequestHandler<GetAllOwnersQuery, ApiResult<List<OwnerDto>>>
    {
        private readonly IOwnerRepository _repository;
        private readonly OwnerMapperApp _mapper;

        public GetAllOwnersQueryHandler(
            IOwnerRepository repository,
            OwnerMapperApp mapper )
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ApiResult<List<OwnerDto>>> Handle(GetAllOwnersQuery request, CancellationToken cancellationToken)
        {
            var owners = await _repository.GetAllAsync();
            var ownersDto = owners.Select(_mapper.ToDto).ToList();

            var message = ownersDto.Any()
                ? "Lista de dueños"
                : "Aún no hay dueños registrados";

            return new ApiResult<List<OwnerDto>>(200, ownersDto, message);
        }
    }
}
