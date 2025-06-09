using Hangfire;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using StockNotifier.Application.Options;
using StockNotifier.Application.Repositories;
using StockNotifier.Infrastructure.Database.Connections;
using StockNotifier.Infrastructure.Database.Dapper;
using StockNotifier.Infrastructure.Database.Jobs;
using StockNotifier.Infrastructure.Database.Migrator;
using StockNotifier.Infrastructure.Database.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RecurringJob = StockNotifier.Infrastructure.Database.Jobs.RecurringJob;

namespace StockNotifier.Infrastructure
{
    public static partial class BuilderExtensions
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            var dbOptions = configuration.GetSection(DatabaseOptions.SectionName).Get<DatabaseOptions>();
            
            var connectionString = new DatabaseConnectionStringProvider(dbOptions, configuration).GetConnectionString();

            services.AddSingleton(dbOptions)
                .AddScoped<IDapperDataContext, DapperDataContext>()
                .AddScoped<IDatabaseConnection, DatabaseConnection>()
                .AddScoped<IDbMigrator, DbMigrator>();

            services.AddHealthChecks()
                .AddSqlServer(connectionString, "sqlserver");

            services.AddScoped<IAlertRepository, AlertRepository>();
            services.AddScoped<IStockRepository, StockRepository>();

            services.AddHangfire(x => x.UseSqlServerStorage(connectionString));
            services.AddHangfireServer();

            services.AddScoped<IRecurringJob, RecurringJob>();
            services.AddHostedService<RecurringJobRegistrationService>();

            var serviceProviderFactory = new DefaultServiceProviderFactory();
            var serviceProvider = serviceProviderFactory.CreateServiceProvider(services);
            var dbMigrator = serviceProvider.GetRequiredService<IDbMigrator>();
            dbMigrator.Migrate();

            return services;
        }
    }
}
