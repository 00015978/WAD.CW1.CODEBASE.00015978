using Application.Interfaces;
using Infrastructure.Persistance.Data;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class Configuration
    {
        public static IServiceCollection InfrastructureService(this IServiceCollection services, IConfiguration configuration)
        {
            //Connection to database
            services.AddDbContext<ApplicationDbContext>(
                options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"))
            );
            // Registering Dependency Injection
            services.AddTransient<IGradeRepository, GradeRepository>();
            services.AddTransient<IStudentRepository, StudentRepository>();
            return services;
        }
    }
}
