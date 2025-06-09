using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockNotifier.Infrastructure.Database.Jobs
{
    public class RecurringJobRegistrationService : IHostedService
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public RecurringJobRegistrationService(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            using IServiceScope scope = _serviceScopeFactory.CreateScope(); 
            var recurringJob = scope.ServiceProvider.GetRequiredService<IRecurringJob>();
            recurringJob.RegisterRecurringJob();
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
    }
}
