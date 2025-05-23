using MediatR;
using Shared.Responses;
using Application.ModuleClient.Services.Dtos;

namespace Application.ModuleClient.Services.Queries.GetAllSevicesType
{
    public class GetAllSevicesTypeQuery : IRequest<ApiResult<List<ServiceTypeDto>>> { }
}
