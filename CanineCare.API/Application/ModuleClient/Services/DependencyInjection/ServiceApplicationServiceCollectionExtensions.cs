using Microsoft.Extensions.DependencyInjection;
using Application.ModuleClient.Services.Mappers;
using Application.ModuleClient.Services.Validator;

namespace Application.ModuleClient.Services.DependencyInjection
{
    public static class ServiceApplicationServiceCollectionExtensions
    {
        public static void AddServiceApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<ServiceMapperApp>();
            services.AddScoped<ServiceValidator>();
        }
    }
}
