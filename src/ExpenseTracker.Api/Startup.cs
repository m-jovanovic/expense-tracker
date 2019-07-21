using ExpenseTracker.Application.Abstractions;
using ExpenseTracker.Application.Behaviors;
using ExpenseTracker.Application.Infrastructure;
using ExpenseTracker.Application.Users.Commands;
using ExpenseTracker.Application.Users.Commands.CreateUser;
using ExpenseTracker.Domain.Abstractions;
using ExpenseTracker.Domain.Aggregates.ExpenseAggregate;
using ExpenseTracker.Domain.Aggregates.UserAggregate;
using ExpenseTracker.Infrastructure;
using ExpenseTracker.Infrastructure.Repository;
using ExpenseTracker.Persistence;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: ApiController]
namespace ExpenseTracker.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var connectionString = new ConnectionString(Configuration["ConnectionString"]);

            services.AddSingleton(connectionString);

            services.AddDbContext<ExpenseTrackerDbContext>(options =>
                options.UseSqlServer(connectionString));

            services.AddMediatR(typeof(CreateUserCommand));
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(UnitOfWorkBehavior<,>));

            services.AddTransient<IDateTime, MachineDateTime>();

            services.AddScoped<IDbContext>(factory => factory.GetRequiredService<ExpenseTrackerDbContext>());
            services.AddScoped<IUnitOfWork>(factory => factory.GetRequiredService<ExpenseTrackerDbContext>());
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IExpenseRepository, ExpenseRepository>();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseMvc();
        }
    }
}
