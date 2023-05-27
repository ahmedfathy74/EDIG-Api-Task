using BusinessLayer.Services.OrderService;
using BusinessLayer.Services.StockService;
using DataAccessLayer.Data;
using DataAccessLayer.Dtos;
using DataAccessLayer.Models;
using EDIG_Api_Task.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace EDIG_Api_Task.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        private readonly IOrderService _orderService;

        public OrderController(ApplicationDbContext context, IOrderService orderService)
        {
            _orderService = orderService;
            _context = context;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Order>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrderList()
        {
            var Orders = await _orderService.GetAllOrders(new[] { "Stock" });
            return Ok(Orders);
        }
      

        [HttpPost]
        [ProducesResponseType(typeof(Order), StatusCodes.Status201Created)]
        public async Task<ActionResult> CreateOrder(OrderDto Orderdto)
        {
            var currentStock = _context.Stocks.Find(Orderdto.StockID); 
            if(currentStock == null) {
                return BadRequest();
            }
            var order = new Order()
            {
                PersonName = Orderdto.PersonName,
                Quantity = Orderdto.Quantity,
                Price = currentStock.Price,
                StockID = Orderdto.StockID
            };
            await _orderService.AddOrder(order);
            return Ok(order);
        }
        // for admin only
        [HttpPut]
        [ProducesResponseType(typeof(Order), StatusCodes.Status201Created)]
        public async Task<ActionResult> UpdateOrder(OrderDto DepDto)
        {
            var order = await _orderService.GetOrderByID(DepDto.OrderId);
            if (order is null)
            {
                return NotFound($"No order Was Found with ID: {DepDto.OrderId}");
            }
            order.PersonName= DepDto.PersonName;
            order.Quantity = DepDto.Quantity;
            order.StockID = DepDto.StockID;

            return Ok(await _orderService.UpdateOrder(order));
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteOrder(int id)
        {
            var order = await _orderService.GetOrderByID(id);
            if (order is null)
            {
                return NotFound($"No Employee Was Found with ID: {id}");
            }
            await _orderService.DeleteOrder(id);
            return Ok();
        }

    }
}
