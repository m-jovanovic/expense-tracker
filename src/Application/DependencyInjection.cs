using Application.Abstractions;
using Application.Behaviors;
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
            services.AddAutoMapper(Documents.AssemblyProvider.GetDocumentsAssembly());
            services.AddMediatR(
                Commands.AssemblyProvider.GetCommandsAssembly(),
                Queries.AssemblyProvider.GetQueriesAssembly(),
                Events.AssemblyProvider.GetEventsAssembly());
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(PerformanceMonitorBehavior<,>));
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(UnitOfWorkBehavior<,>));

            ServiceProvider serviceProvider = services.BuildServiceProvider();

            var documentStoreProvider = serviceProvider.GetService<IDocumentStoreProvider>();

            IndexCreation.CreateIndexes(Queries.AssemblyProvider.GetQueriesAssembly(), documentStoreProvider.GetDocumentStore());

            return services;
        }
    }
}
