using MediatR;
using Shared.Responses;
using Application.ModuleClient.Services.Dtos;
using Domain.ModuleClient.Services.Repositories;
using Application.ModuleClient.Services.Mappers;
using Application.ModuleClient.Services.Validator;

namespace Application.ModuleClient.Services.Queries.GetServiceById
{
    public class GetServiceByIdQueryHandler : IRequestHandler<GetServiceByIdQuery, ApiResult<ServiceDto>>
    {
        private readonly IServiceRepository _repository;
        private readonly ServiceValidator _validator;
        private readonly ServiceMapperApp _mapper;

        public GetServiceByIdQueryHandler(
            IServiceRepository repository,
            ServiceValidator validator,
            ServiceMapperApp mapper )
        {
            _repository = repository;
            _validator = validator;
            _mapper = mapper;
        }

        public async Task<ApiResult<ServiceDto>> Handle(GetServiceByIdQuery request, CancellationToken cancellationToken)
        {
            await _validator.ValidateGetByIdAsync(request.Id);
            var service = await _repository.GetByIdAsync(request.Id);
            var serviceDto = _mapper.ToDto(service);
            return new ApiResult<ServiceDto>(200, serviceDto, "Información del servicio.");
        }
    }
}
