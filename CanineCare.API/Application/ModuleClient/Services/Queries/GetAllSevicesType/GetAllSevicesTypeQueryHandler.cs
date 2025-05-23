using MediatR;
using Shared.Responses;
using Application.ModuleClient.Services.Dtos;
using Domain.ModuleClient.Services.Repositories;

namespace Application.ModuleClient.Services.Queries.GetAllSevicesType
{
    public class GetAllSevicesTypeQueryHandler : IRequestHandler<GetAllSevicesTypeQuery, ApiResult<List<ServiceTypeDto>>>
    {
        private readonly IServiceRepository _repository;

        public GetAllSevicesTypeQueryHandler(IServiceRepository repository)
        {
            _repository = repository;
        }

        public async Task<ApiResult<List<ServiceTypeDto>>> Handle(GetAllSevicesTypeQuery request, CancellationToken cancellationToken)
        {
            var types = await _repository.GetAllServiceTypes();

            var typesDto = types
                .Select(t => new ServiceTypeDto { Type = t })
                .ToList();

            return new ApiResult<List<ServiceTypeDto>>(200, typesDto, "Lista de typos de servicios");
        }
    }
}
