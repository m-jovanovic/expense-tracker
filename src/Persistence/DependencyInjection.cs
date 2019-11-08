using Application.Abstractions;
using Domain.Budgets;
using Domain.Expenses;
using Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Repository;

namespace Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ExpenseTrackerDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("ExpenseTrackerDb"));
            });
            services.AddScoped<IDbContext>(factory => factory.GetRequiredService<ExpenseTrackerDbContext>());
            services.AddScoped<IUnitOfWork>(factory => factory.GetRequiredService<ExpenseTrackerDbContext>());
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IExpenseRepository, ExpenseRepository>();
            services.AddScoped<IBudgetRepository, BudgetRepository>();

            return services;
        }
    }
}
