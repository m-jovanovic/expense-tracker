using System.Reflection;
using Application.Abstractions;
using Application.Behaviours;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Raven.Client.Documents.Indexes;

namespace Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(LoggingBehaviour<,>));
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(PerformanceMonitorBehaviour<,>));
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(UnitOfWorkBehaviour<,>));

            ServiceProvider serviceProvider = services.BuildServiceProvider();

            var documentStoreProvider = serviceProvider.GetService<IDocumentStoreProvider>();

            IndexCreation.CreateIndexes(Assembly.GetExecutingAssembly(), documentStoreProvider.GetDocumentStore());

            return services;
        }
    }
}
