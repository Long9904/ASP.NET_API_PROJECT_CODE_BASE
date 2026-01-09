using Application.Common.Results;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Application
{
    public static class DependencyInjection 
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            // Register all validators from the assembly
            services.AddValidatorsFromAssemblyContaining(typeof(DependencyInjection));

            // MediatR
            services.AddMediatR(cfg => 
            {
                cfg.RegisterServicesFromAssemblyContaining(typeof(DependencyInjection));
            });

            // Validation Behavior for MediatR
            // Command -> MediatR Pipeline --> ValidationBehavior --> Handler
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

            return services;
        }
    }
}
