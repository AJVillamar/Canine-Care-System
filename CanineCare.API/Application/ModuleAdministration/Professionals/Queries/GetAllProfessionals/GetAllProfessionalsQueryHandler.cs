using MediatR;
using Shared.Responses;
using Application.ModuleAdministration.Professionals.Dtos;
using Domain.ModuleAdministration.Professionals.Repositories;
using Application.ModuleAdministration.Professionals.Mappers;

namespace Application.ModuleAdministration.Professionals.Queries.GetAllProfessionals
{
    public class GetAllProfessionalsQueryHandler : IRequestHandler<GetAllProfessionalsQuery, ApiResult<List<ProfessionalDto>>>
    {
        private readonly IProfessionalRepository _repository;
        private readonly ProfessionalMapperApp _mapper;

        public GetAllProfessionalsQueryHandler(
            IProfessionalRepository repository,
            ProfessionalMapperApp mapper )
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ApiResult<List<ProfessionalDto>>> Handle(GetAllProfessionalsQuery request, CancellationToken cancellationToken)
        {
            var professionals = await _repository.GetAllAsync();
            var professionalsDto = professionals.Select(_mapper.ToDto).ToList();

            var message = professionalsDto.Any()
                ? "Lista de profesionales"
                : "Aún no hay profesionales registrados";

            return new ApiResult<List<ProfessionalDto>>(200, professionalsDto, message);
        }
    }
}
