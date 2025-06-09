using StockNotifier.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockNotifier.Application.Repositories
{
    public interface IStockRepository
    {
        Task CreateUpdateStock(Stock stock);
        Stock GetStock(int id);
        Task<List<Stock>> GetStocksAsync();
        Stock GetStockByName(string name);
        void DeleteStock(int id);
    }
}
