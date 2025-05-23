using MediatR;
using Shared.Responses;
using Application.ModuleClient.Pets.Dtos;

namespace Application.ModuleClient.Pets.Queries.GetPetById
{
    public class GetPetByIdQuery : IRequest<ApiResult<PetDto>>
    {
        public Guid Id { get; set; }
    }
}
