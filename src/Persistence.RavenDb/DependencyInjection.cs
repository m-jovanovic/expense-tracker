﻿using Application.Abstractions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Persistence.RavenDb
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddRavenDb(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<RavenDbSettings>(configuration.GetSection(nameof(RavenDbSettings)));
            services.AddSingleton<IDocumentStoreProvider, DocumentStoreProvider>();
            services.AddScoped(serviceProvider => serviceProvider.GetRequiredService<IDocumentStoreProvider>()
                .GetDocumentStore().OpenAsyncSession());

            return services;
        }
    }
}
