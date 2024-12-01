using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Application
{
    public static class Configuration
    {
        public static IServiceCollection ApplicationService(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            return services;
        }
    }
}
