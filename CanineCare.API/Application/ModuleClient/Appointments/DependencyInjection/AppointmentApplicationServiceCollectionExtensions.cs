using Microsoft.Extensions.DependencyInjection;
using Application.ModuleClient.Appointments.Mappers;
using Application.ModuleClient.Appointments.Validators;

namespace Application.ModuleClient.Appointments.DependencyInjection
{
    public static class AppointmentApplicationServiceCollectionExtensions
    {
        public static void AddAppointmentApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<AppointmentMapperApp>();
            services.AddScoped<AppointmentValidator>();
        }
    }
}
