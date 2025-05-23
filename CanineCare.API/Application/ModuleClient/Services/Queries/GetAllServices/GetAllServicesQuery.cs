using MediatR;
using Shared.Responses;
using Application.ModuleClient.Services.Dtos;

namespace Application.ModuleClient.Services.Queries.GetAllServices
{ 
    public class GetAllServicesQuery : IRequest<ApiResult<List<ServiceDto>>> { }
}
