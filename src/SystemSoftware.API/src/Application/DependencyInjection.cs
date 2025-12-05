using Microsoft.Extensions.DependencyInjection;
using MediatR;
using System.Reflection;
using SystemSoftware.Application.Common.Behaviors;

namespace SystemSoftware.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehavior<,>));
            
            return services;
        }
    }
}
