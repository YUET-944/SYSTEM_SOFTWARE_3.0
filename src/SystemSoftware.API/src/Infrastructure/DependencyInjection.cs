using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SystemSoftware.Application.Common.Interfaces;
using SystemSoftware.Infrastructure.Persistence;
using SystemSoftware.Infrastructure.Services;
using SystemSoftware.Domain.Interfaces;

namespace SystemSoftware.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            // Database
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection"),
                    b => b.MigrationsAssembly("SystemSoftware.WebAPI")));

            // Services
            services.AddScoped<Domain.Interfaces.IPasswordHasher, PasswordHasher>();
            services.AddScoped<IApplicationDbContext, ApplicationDbContext>();
            services.AddScoped<IJwtService, JwtService>();

            return services;
        }
    }
}
