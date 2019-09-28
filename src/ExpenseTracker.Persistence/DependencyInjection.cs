using ExpenseTracker.Application.Abstractions;
using ExpenseTracker.Application.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ExpenseTracker.Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = new ConnectionString(configuration.GetConnectionString("ExpenseTrackerDb"));

            services.AddSingleton(connectionString);

            services.AddDbContext<ExpenseTrackerDbContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });

            services.AddScoped<IDbContext>(factory => factory.GetRequiredService<ExpenseTrackerDbContext>());

            services.AddScoped<IUnitOfWork>(factory => factory.GetRequiredService<ExpenseTrackerDbContext>());

            return services;
        }
    }
}
