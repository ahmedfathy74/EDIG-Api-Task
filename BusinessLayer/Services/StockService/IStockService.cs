using DataAccessLayer.Repositors.Base;
using EDIG_Api_Task.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services.StockService
{
    public interface IStockService 
    {
        Task<IEnumerable<Stock>> GetAllStocks();
        Task AddStock(Stock stock);
        Task<Stock> Updatestock(Stock stock);
        Task DeleteStock(int id);
    }
}
