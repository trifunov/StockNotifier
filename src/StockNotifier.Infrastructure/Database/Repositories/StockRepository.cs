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
    internal class StockRepository : IStockRepository
    {
        private readonly IDapperDataContext _dapperDataContext;

        public StockRepository(IDapperDataContext dapperDataContext)
        {
            _dapperDataContext = dapperDataContext;
        }

        public async Task CreateUpdateStock(Stock stock)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@Name", stock.Name, DbType.String, ParameterDirection.Input);
            parameters.Add("@Value", stock.Value, DbType.Int32, ParameterDirection.Input);
            parameters.Add("@CreatedAt", stock.CreatedAt, DbType.DateTime, ParameterDirection.Input);
            parameters.Add("@ModifiedAt", stock.ModifiedAt, DbType.DateTime, ParameterDirection.Input);

            await _dapperDataContext.Connection!.QueryAsync
            (
                sql: $"UpsertStock",
                param: parameters,
                commandType: CommandType.StoredProcedure,
                transaction: _dapperDataContext.Transaction,
                commandTimeout: _dapperDataContext.Connection!.ConnectionTimeout
            ).ConfigureAwait(false);
        }

        public void DeleteStock(int id)
        {
            throw new NotImplementedException();
        }

        public Stock GetStock(int id)
        {
            throw new NotImplementedException();
        }

        public Stock GetStockByName(string name)
        {
            throw new NotImplementedException();
        }

        public List<Stock> GetStocks()
        {
            throw new NotImplementedException();
        }
    }
}
