using MediatR;
using Shared.Responses;
using Application.ModuleClient.Services.Mappers;
using Domain.ModuleClient.Services.Repositories;
using Application.ModuleClient.Services.Validator;

namespace Application.ModuleClient.Services.Commands.CreateService
{
    public class CreateServiceCommandHandler : IRequestHandler<CreateServiceCommand, ApiResult>
    {
        private readonly IServiceRepository _repository;
        private readonly ServiceValidator _validator;
        private readonly ServiceMapperApp _mapper;

        public CreateServiceCommandHandler(
            IServiceRepository repository,
            ServiceValidator validator,
            ServiceMapperApp mapper )
        {
            _repository = repository;
            _validator = validator;
            _mapper = mapper;
        }
        
        public async Task<ApiResult> Handle(CreateServiceCommand request, CancellationToken cancellationToken)
        {
            await _validator.ValidateCreateAsync(request.Name);
            var service = _mapper.ToDomain(request);
            await _repository.AddAsync(service);
            return new ApiResult(201, "Servicio de desparasitación creado con éxito.");
        }
    }
}
