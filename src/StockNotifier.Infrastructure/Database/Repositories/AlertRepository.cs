using Dapper;
using StockNotifier.Application.Repositories;
using StockNotifier.Domain.Entities;
using StockNotifier.Infrastructure.Database.Dapper;
using System;
using System.Collections.Generic;
using System.Data;
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
            var parameters = new DynamicParameters();
            parameters.Add("@Name", alert.Name, DbType.String, ParameterDirection.Input);
            parameters.Add("@Id", alert.Id, DbType.Int32, ParameterDirection.Input);
            parameters.Add("@CreatedAt", alert.CreatedAt, DbType.DateTime, ParameterDirection.Input);
            parameters.Add("@ModifiedAt", alert.ModifiedAt, DbType.DateTime, ParameterDirection.Input);
            parameters.Add("@StockId", alert.StockId, DbType.Int32, ParameterDirection.Input);
            parameters.Add("@ThresholdType", alert.ThresholdType, DbType.Int32, ParameterDirection.Input);
            parameters.Add("@ThresholdValue", alert.ThresholdValue, DbType.Int32, ParameterDirection.Input);
            parameters.Add("@IsActive", alert.IsActive, DbType.Boolean, ParameterDirection.Input);

            await _dapperDataContext.Connection!.QueryAsync
            (
                sql: $"UpsertAlert",
                param: parameters,
                commandType: CommandType.StoredProcedure,
                transaction: _dapperDataContext.Transaction,
                commandTimeout: _dapperDataContext.Connection!.ConnectionTimeout
            ).ConfigureAwait(false);
        }

        public void DeleteAlert(int id)
        {
            throw new NotImplementedException();
        }

        public Alert GetAlert(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Alert>> GetAlertsAsync()
        {
            var alerts = await _dapperDataContext.Connection!
                .QueryAsync<Alert>(
                    sql: "GetAlerts",
                    param: null,
                    commandType: CommandType.StoredProcedure,
                    transaction: _dapperDataContext.Transaction,
                    commandTimeout: _dapperDataContext.Connection!.ConnectionTimeout
                );
            return alerts.ToList();
        }

        public List<Alert> GetAlertsByStockId(int stockId)
        {
            throw new NotImplementedException();
        }
    }
}
