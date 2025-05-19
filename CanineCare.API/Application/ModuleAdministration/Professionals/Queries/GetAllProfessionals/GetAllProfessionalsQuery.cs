using MediatR;
using Shared.Responses;
using Application.ModuleAdministration.Professionals.Dtos;

namespace Application.ModuleAdministration.Professionals.Queries.GetAllProfessionals
{
    public class GetAllProfessionalsQuery : IRequest<ApiResult<List<ProfessionalDto>>> { }
}
