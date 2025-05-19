using MediatR;
using Shared.Responses;

namespace Application.ModuleClient.Breeds.Commands.CreateBreed
{
    public class CreateBreedCommand : IRequest<ApiResult>
    {
        public string? Name { get; set; }
    }
}
 