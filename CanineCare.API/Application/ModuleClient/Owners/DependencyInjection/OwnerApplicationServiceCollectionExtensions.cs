using Application.ModuleClient.Owners.Mappers;
using Microsoft.Extensions.DependencyInjection;
using Application.ModuleClient.Owners.Validators;

namespace Application.ModuleClient.Owners.DependencyInjection
{
    public static class OwnerApplicationServiceCollectionExtensions
    {
        public static void AddOwnerApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<OwnerMapperApp>();
            services.AddScoped<OwnerValidator>();
        }
    }
}
