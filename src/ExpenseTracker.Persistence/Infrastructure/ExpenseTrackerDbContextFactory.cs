using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace ExpenseTracker.Persistence.Infrastructure
{
    /// <summary>
    /// Represents the <see cref="ExpenseTrackerDbContext"/> design time factory implementation.
    /// </summary>
    public sealed class ExpenseTrackerDbContextFactory : IDesignTimeDbContextFactory<ExpenseTrackerDbContext>
    {
        private const string ConnectionStringName = "ExpenseTrackerDb";
        private const string ApiProjectName = "ExpenseTracker.Api";
        private const string AspNetCoreEnvironment = "ASPNETCORE_ENVIRONMENT";

        /// <inheritdoc />
        public ExpenseTrackerDbContext CreateDbContext(string[] args)
        {
            string basePath = $"{Directory.GetCurrentDirectory()}{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}{ApiProjectName}";

            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable(AspNetCoreEnvironment)}.json", true)
                .AddEnvironmentVariables()
                .Build();

            string connectionString = configuration.GetConnectionString(ConnectionStringName);

            return CreateDbContext(connectionString, basePath);
        }

        /// <summary>
        /// Creates an <see cref="ExpenseTrackerDbContext"/> instance using the specified connection string.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        /// <param name="basePath">The base path.</param>
        /// <returns>The new instance of <see cref="ExpenseTrackerDbContext"/>.</returns>
        /// <exception cref="ArgumentException"> when <paramref name="connectionString"/> is null or whitespace.</exception>
        private static ExpenseTrackerDbContext CreateDbContext(string connectionString, string basePath)
        {
            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new ArgumentException($"Connection string '{ConnectionStringName}' is null or empty. Base path: {basePath}", nameof(connectionString));
            }

            var optionsBuilder = new DbContextOptionsBuilder<ExpenseTrackerDbContext>();

            optionsBuilder.UseSqlServer(connectionString);

            return new ExpenseTrackerDbContext(optionsBuilder.Options);
        }
    }
}
