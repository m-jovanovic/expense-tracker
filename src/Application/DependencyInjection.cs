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
            services.AddAutoMapper(Documents.AssemblyProvider.GetDocumentsAssembly());
            services.AddMediatR(Commands.AssemblyProvider.GetCommandsAssembly(), Queries.AssemblyProvider.GetQueriesAssembly());
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(LoggingBehaviour<,>));
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(PerformanceMonitorBehaviour<,>));
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(UnitOfWorkBehaviour<,>));

            ServiceProvider serviceProvider = services.BuildServiceProvider();

            var documentStoreProvider = serviceProvider.GetService<IDocumentStoreProvider>();

            IndexCreation.CreateIndexes(Queries.AssemblyProvider.GetQueriesAssembly(), documentStoreProvider.GetDocumentStore());

            return services;
        }
    }
}
