using StockNotifier.Application.Repositories;
using StockNotifier.Domain.Entities;
using StockNotifier.Infrastructure.Database.Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockNotifier.Infrastructure.Database.Repositories
{
    public class AlertRepository : IAlertRepository
    {
        private readonly IDapperDataContext _dapperDataContext;

        public AlertRepository(IDapperDataContext dapperDataContext)
        {
            _dapperDataContext = dapperDataContext;
        }

        public async Task CreateUpdateAlertAsync(Alert alert)
        {
            throw new NotImplementedException();
        }

        public void DeleteAlert(int id)
        {
            throw new NotImplementedException();
        }

        public Alert GetAlert(int id)
        {
            throw new NotImplementedException();
        }

        public List<Alert> GetAlerts()
        {
            throw new NotImplementedException();
        }

        public List<Alert> GetAlertsByStockId(int stockId)
        {
            throw new NotImplementedException();
        }
    }
}
