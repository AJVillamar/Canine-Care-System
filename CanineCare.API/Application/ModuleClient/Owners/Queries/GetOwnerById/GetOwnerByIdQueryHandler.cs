using MediatR;
using Shared.Responses;
using Application.ModuleClient.Owners.Dtos;
using Application.ModuleClient.Owners.Mappers;
using Domain.ModuleClient.Owners.Repositories;
using Application.ModuleClient.Owners.Validators;

namespace Application.ModuleClient.Owners.Queries.GetOwnerById
{
    public class GetOwnerByIdentificationQueryHandler : IRequestHandler<GetOwnerByIdQuery, ApiResult<OwnerDto>>
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

        public async Task<ApiResult<OwnerDto>> Handle(GetOwnerByIdQuery request, CancellationToken cancellationToken)
        {
            await _validator.ValidateGetByIdAsync(request.Id);
            var owner = await _repository.GetByIdAsync(request.Id);
            var ownerDto = _mapper.ToDto(owner);
            return new ApiResult<OwnerDto>(200, ownerDto, "Información del dueño.");
        }
    }
}
