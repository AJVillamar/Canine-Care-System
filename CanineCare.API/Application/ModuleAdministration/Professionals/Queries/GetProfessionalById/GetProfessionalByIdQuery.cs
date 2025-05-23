using MediatR;
using Shared.Responses;
using Application.ModuleAdministration.Professionals.Dtos;

namespace Application.ModuleAdministration.Professionals.Queries.GetProfessionalById
{
    public class GetProfessionalByIdQuery : IRequest<ApiResult<ProfessionalDto>>
    {
        public Guid ProfessionalId { get; set; }
    }
}
