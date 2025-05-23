using MediatR;
using Shared.Responses;
using Application.ModuleClient.Services.Dtos;
using Application.ModuleClient.Services.Mappers;
using Domain.ModuleClient.Services.Repositories;

namespace Application.ModuleClient.Services.Queries.GetAllServices
{
    public class GetAllServicesQueryHandler : IRequestHandler<GetAllServicesQuery, ApiResult<List<ServiceDto>>>
    {
        private readonly IServiceRepository _repository;
        private readonly ServiceMapperApp _mapper;

        public GetAllServicesQueryHandler(
            IServiceRepository repository,
            ServiceMapperApp mapper )
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ApiResult<List<ServiceDto>>> Handle(GetAllServicesQuery request, CancellationToken cancellationToken)
        {
            var services = await _repository.GetAllAsync();
            var serviceDtos = services.Select(_mapper.ToDto).ToList();

            var message = serviceDtos.Any()
                ? "Lista de servicios"
                : "Aún no hay servicios registrados";

            return new ApiResult<List<ServiceDto>>(200, serviceDtos, message);
        }
    }
}
