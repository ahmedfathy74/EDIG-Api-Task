using DataAccessLayer.Data;
using EDIG_Api_Task.Entity;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models
{
    public class StockPriceHub : Hub
    {
        //public async Task SendStockPriceUpdate(IEnumerable<Stock> stocks)
        //{
        //    await Clients.All.SendAsync("ReceiveStockPriceUpdate", stocks);
        //}
        private readonly ApplicationDbContext  _context;
        private readonly Random _random;

        public StockPriceHub(ApplicationDbContext context )
        {
            _context = context;
            _random = new Random();
        }

        public override async Task OnConnectedAsync()
        {
            try
            {
                await base.OnConnectedAsync();

                // Start sending stock price updates every 10 seconds
                while (true)
                {
                    var stocks = await _context.Stocks.ToListAsync();
                    foreach (var stock in stocks)
                    {
                        stock.Price = _random.Next(1, 100);
                    }
                    await _context.SaveChangesAsync();
                    await Clients.All.SendAsync("ReceiveStockPriceUpdate", stocks);
                    await Task.Delay(TimeSpan.FromSeconds(10));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"SignalR connection error: {ex.Message}");
            }
        }
    }
}
