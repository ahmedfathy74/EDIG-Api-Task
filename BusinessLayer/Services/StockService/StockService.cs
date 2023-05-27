using DataAccessLayer.Repositors.Base;
using EDIG_Api_Task.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services.StockService
{
    public class StockService : IStockService
    {
        private readonly IGenericRepo<Stock> _stockService;
        public StockService(IGenericRepo<Stock> stockService)
        {
            _stockService = stockService;
        }
        public async Task<IEnumerable<Stock>> GetAllStocks()
        {
            try
            {
                return await _stockService.GetAllEntries();
            }
            catch (Exception)
            {
                throw new Exception("Error , for get all stocks, try again later plz");
            }
        }
        public async Task AddStock(Stock stock)
        {
            try
            {
                if (stock is null)
                {
                    throw new ArgumentNullException(nameof(stock));
                }
                else
                {
                    await _stockService.AddNewOne(stock);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

        }
        public async Task<Stock> Updatestock(Stock stock)
        {
            try
            {
                if (stock is null)
                {
                    throw new ArgumentNullException(nameof(stock));
                }
                else
                {
                    return await _stockService.UpdateOne(stock);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task DeleteStock(int id)
        {
            try
            {
                await _stockService.DeleteOne(id);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
