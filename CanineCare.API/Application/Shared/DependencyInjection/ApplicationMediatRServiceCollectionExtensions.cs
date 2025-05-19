using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Shared.DependencyInjection
{
    public static class ApplicationMediatRServiceCollectionExtensions
    {
        public static void AddApplicationMediatR(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        }
    }
}
