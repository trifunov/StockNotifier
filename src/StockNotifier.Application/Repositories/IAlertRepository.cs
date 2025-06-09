using StockNotifier.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockNotifier.Application.Repositories
{
    public interface IAlertRepository
    {
        Task CreateUpdateAlertAsync(Alert alert);
        Alert GetAlert(int id);
        Task<List<Alert>> GetAlertsAsync();
        List<Alert> GetAlertsByStockId(int stockId);
        void DeleteAlert(int id);
    }
}
