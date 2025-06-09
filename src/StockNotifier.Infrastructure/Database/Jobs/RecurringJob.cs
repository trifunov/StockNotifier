using Hangfire;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;
using StockNotifier.Application.Core.Cache;
using StockNotifier.Application.Repositories;
using StockNotifier.Domain.Entities;
using StockNotifier.Domain.Enums;
using StockNotifier.Infrastructure.SignalRServer;

namespace StockNotifier.Infrastructure.Database.Jobs
{
    public class RecurringJob : IRecurringJob
    {
        private readonly IStockRepository _stockRepository;
        private readonly IAlertRepository _alertRepository;
        private readonly IConfiguration _configuration;
        private readonly ICacheService _cacheService;
        private readonly IHubContext<AlertHub> _alertHubContext;

        public RecurringJob(IStockRepository stockRepository, IConfiguration configuration, IAlertRepository alertRepository, ICacheService cacheService, IHubContext<AlertHub> hubContext)
        {
            _stockRepository = stockRepository;
            _configuration = configuration;
            _alertRepository = alertRepository;
            _cacheService = cacheService;
            _alertHubContext = hubContext;
        }

        public void RegisterRecurringJob()
        {
            Hangfire.RecurringJob.AddOrUpdate(
            "recurring-job-stocks",
            () => ExecuteStocks(),
            Cron.Minutely);
        }

        public async Task ExecuteStocks()
        {
            var stocks = _configuration["PredefinedStocks"].Split(',');

            foreach (var stock in stocks)
            {
                var stockEntity = new Stock
                {
                    Name = stock.Trim(),
                    Value = new Random().Next(1, 200),
                    CreatedAt = DateTime.UtcNow,
                    ModifiedAt = DateTime.UtcNow
                };
                await _stockRepository.CreateUpdateStock(stockEntity);
            }

            List<Stock> stockEntities = await _stockRepository.GetStocksAsync();
            await _cacheService.SetAsync("Stocks", stockEntities, TimeSpan.FromMinutes(20));

            await ExecuteAlerts();
        }

        public async Task ExecuteAlerts()
        {
            var stocks = await _cacheService.GetAsync<List<Stock>>("Stocks");
            if (stocks == null || !stocks.Any())
            {
                stocks = await _stockRepository.GetStocksAsync();
                await _cacheService.SetAsync("Stocks", stocks, TimeSpan.FromMinutes(20));
            }

            var alerts = await _cacheService.GetAsync<List<Alert>>("Alerts");
            if (alerts == null || !alerts.Any())
            {
                alerts = await _alertRepository.GetAlertsAsync();
                await _cacheService.SetAsync("Alerts", alerts, TimeSpan.FromHours(2));
            }

            foreach (var alert in alerts)
            {
                var stock = stocks.FirstOrDefault(s => s.Id == alert.StockId);
                if (stock != null)
                {
                    if (alert.ThresholdType == ThresholdType.Over && stock.Value > alert.ThresholdValue)
                    {
                        await _alertHubContext.Clients.All.SendAsync("ReceiveOverAlert", alert);
                    }

                    if (alert.ThresholdType == ThresholdType.Under && stock.Value < alert.ThresholdValue)
                    {
                        await _alertHubContext.Clients.All.SendAsync("ReceiveUnderAlert", alert);
                    }

                    if (alert.ThresholdType == ThresholdType.Equal && stock.Value == alert.ThresholdValue)
                    {
                        await _alertHubContext.Clients.All.SendAsync("ReceiveEqualAlert", alert);
                    }
                }
            }
        }
    }
}
