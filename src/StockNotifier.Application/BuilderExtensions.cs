using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StockNotifier.Application.Core.Command;
using StockNotifier.Application.Core.Query;
using StockNotifier.Application.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace StockNotifier.Application
{
    public static partial class BuilderExtensions
    {
        private static Assembly ApplicationAssembly => typeof(BuilderExtensions).Assembly;

        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(ApplicationAssembly))
            .AddScoped<ICommandDispatcher, CommandDispatcher>()
            .AddScoped<IQueryDispatcher, QueryDispatcher>(); 

            return services;
        }
    }
}
