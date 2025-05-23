using MediatR;
using Shared.Responses;
using Application.ModuleAdministration.Professionals.Dtos;
using Application.ModuleAdministration.Professionals.Mappers;
using Domain.ModuleAdministration.Professionals.Repositories;
using Application.ModuleAdministration.Professionals.Validators;

namespace Application.ModuleAdministration.Professionals.Queries.GetProfessionalById
{
    public class GetProfessionalByIdQueryHandler : IRequestHandler<GetProfessionalByIdQuery, ApiResult<ProfessionalDto>>
    {
        private readonly IProfessionalRepository _repository;
        private readonly ProfessionalValidator _validator;
        private readonly ProfessionalMapperApp _mapper;

        public GetProfessionalByIdQueryHandler(
            IProfessionalRepository repository,
            ProfessionalValidator validator, 
            ProfessionalMapperApp mapper )
        {
            _repository = repository;
            _validator = validator;
            _mapper = mapper;
        }

        public async Task<ApiResult<ProfessionalDto>> Handle(GetProfessionalByIdQuery request, CancellationToken cancellationToken)
        {
            await _validator.ValidateGetByIdAsync(request.ProfessionalId);
            var professional = await _repository.GetByIdAsync(request.ProfessionalId);
            var professionalDto = _mapper.ToDto(professional);
            return new ApiResult<ProfessionalDto>(200, professionalDto, "Información del profesional.");
        }
    }
}
