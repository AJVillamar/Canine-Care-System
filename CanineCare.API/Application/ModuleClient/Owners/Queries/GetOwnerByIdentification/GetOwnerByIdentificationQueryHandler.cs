using MediatR;
using Shared.Responses;
using Application.ModuleClient.Owners.Dtos;
using Application.ModuleClient.Owners.Mappers;
using Domain.ModuleClient.Owners.Repositories;
using Application.ModuleClient.Owners.Validators;

namespace Application.ModuleClient.Owners.Queries.GetOwnerByIdentification
{
    public class GetOwnerByIdentificationQueryHandler : IRequestHandler<GetOwnerByIdentificationQuery, ApiResult<OwnerDto>>
    {
        private readonly IOwnerRepository _repository;
        private readonly OwnerValidator _validator;
        private readonly OwnerMapperApp _mapper;

        public GetOwnerByIdentificationQueryHandler(
            IOwnerRepository repository, 
            OwnerValidator validator, 
            OwnerMapperApp mapper )
        {
            _repository = repository;
            _validator = validator;
            _mapper = mapper;
        }

        public async Task<ApiResult<OwnerDto>> Handle(GetOwnerByIdentificationQuery request, CancellationToken cancellationToken)
        {
            await _validator.ValidateGetByIdentificationAsync(request.Identification);
            var owner = await _repository.GetByIdentificationAsync(request.Identification);
            var ownerDto = _mapper.ToDto(owner);
            return new ApiResult<OwnerDto>(200, ownerDto, "Información del dueño.");
        }
    }
}
