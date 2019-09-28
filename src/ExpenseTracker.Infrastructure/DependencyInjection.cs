using ExpenseTracker.Application.Abstractions;
using ExpenseTracker.Domain.Aggregates.Expenses;
using ExpenseTracker.Domain.Aggregates.Users;
using ExpenseTracker.Infrastructure.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace ExpenseTracker.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IExpenseRepository, ExpenseRepository>();
            services.AddTransient<IDateTime, MachineDateTime>();
            services.AddTransient<IDbConnectionFactory, SqlServerDbConnectionFactory>();

            return services;
        }
    }
}
