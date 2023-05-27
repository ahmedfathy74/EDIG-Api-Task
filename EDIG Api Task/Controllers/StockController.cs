using BusinessLayer.Services.StockService;
using DataAccessLayer.Data;
using DataAccessLayer.Dtos;
using DataAccessLayer.Models;
using EDIG_Api_Task.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System;

namespace EDIG_Api_Task.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StockController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        private readonly IStockService _stockService;
        private readonly Random _random;
        private readonly IHubContext<StockPriceHub> _stockPriceHubContext;
        private readonly Timer _timer;
        private readonly object _lock;

        public StockController(ApplicationDbContext context,IStockService stockService, IHubContext<StockPriceHub> stockPriceHubContext)
        {
            _context = context;
            _stockService = stockService;
            _random = new Random();
            _stockPriceHubContext = stockPriceHubContext;
            _lock = new object();
            //InitializeStockPrices();

            // Initialize the timer to update stock prices every 10 seconds
            //_timer = new Timer(UpdateStockPrices, null, TimeSpan.Zero, TimeSpan.FromSeconds(10));
        }

        private void InitializeStockPrices()
        {
            var stocks = _context.Stocks.ToList();

            foreach (var stock in stocks)
            {
                stock.Price = GenerateRandomPrice();
            }
            _context.SaveChanges();
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Stock>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Stock>>> GetStockList()
        {
            var Stocks = await _stockService.GetAllStocks();    
            return Ok(Stocks);
        }
        private void UpdateStockPrices(object state)
        {
            try
            {
                var stocks = _context.Stocks.ToList();
                foreach (var stock in stocks)
                {
                    stock.Price = GenerateRandomPrice();
                    //_stockPriceHubContext.Clients.All.SendAsync("ReceiveStockPriceUpdate", stock.Id, stock.Price);
                    _stockPriceHubContext.Clients.All.SendAsync("ReceiveStockPriceUpdate", stocks); 
                }
                _context.SaveChanges();
            }
            catch (Exception ex) { 
                Console.WriteLine(ex.ToString());   
            }
        }
        // if admin want to Add stocks or any one
        [HttpPost]
        [ProducesResponseType(typeof(Stock), StatusCodes.Status201Created)]
        public async Task<ActionResult> CreateStock(StockDto stockdto)
        {
            var stock = new Stock() { StockName = stockdto.StockName};
            await _stockService.AddStock(stock);
            return Ok(stock);   
        }
        private decimal GenerateRandomPrice()
        {
            return _random.Next(1, 101); // Generates a random number between 1 and 100 (inclusive)
        }
    }
}
