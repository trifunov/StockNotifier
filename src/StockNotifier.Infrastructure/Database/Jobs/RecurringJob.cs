using Hangfire;
using Microsoft.Extensions.Configuration;
using StockNotifier.Application.Repositories;
using StockNotifier.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockNotifier.Infrastructure.Database.Jobs
{
    public class RecurringJob : IRecurringJob
    {
        private readonly IStockRepository _stockRepository;
        private readonly IConfiguration _configuration;

        public RecurringJob(IStockRepository stockRepository, IConfiguration configuration)
        {
            _stockRepository = stockRepository;
            _configuration = configuration;
        }

        public void RegisterRecurringJob()
        {
            Hangfire.RecurringJob.AddOrUpdate(
            "recurring-job-stocks",
            () => Execute(),
            Cron.Minutely);
        }

        public void Execute()
        {
            var stocks = _configuration["PredefinedStocks"].Split(','); // Accessing a string from appsettings.json  

            foreach (var stock in stocks)
            {
                var stockEntity = new Stock
                {
                    Name = stock.Trim(),
                    Value = new Random().Next(1, 200),
                    CreatedAt = DateTime.UtcNow,
                    ModifiedAt = DateTime.UtcNow
                };
                _stockRepository.CreateUpdateStock(stockEntity);
            }
        }
    }
}
