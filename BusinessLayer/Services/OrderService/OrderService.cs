using DataAccessLayer.Repositors.Base;
using EDIG_Api_Task.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services.OrderService
{
    public class OrderService:IOrderService
    {
        private readonly IGenericRepo<Order> _orderService;
        public OrderService(IGenericRepo<Order> orderService)
        {
            _orderService = orderService;
        }
        public async Task<IEnumerable<Order>> GetAllOrders(string[]? includes = null)
        {
            try
            {
                return await _orderService.GetAllEntries(includes);
            }
            catch (Exception)
            {
                throw new Exception("Error , for get all orders, try again later plz");
            }
        }
        public async Task AddOrder(Order order)
        {
            try
            {
                if(order is null)
                {
                    throw new ArgumentNullException(nameof(order));
                }
                else
                {
                     await _orderService.AddNewOne(order);
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
            
        }
        public async Task<Order> UpdateOrder(Order order)
        {
            try
            {
                if (order is null)
                {
                    throw new ArgumentNullException(nameof(order));
                }
                else
                {
                    return await _orderService.UpdateOne(order);
                }
            }
            catch(Exception)
            {
                throw;
            }
        }
        public async Task DeleteOrder(int id)
        {
            try
            {
                await _orderService.DeleteOne(id);
            }
            catch(Exception)
            {
                throw;
            }
        }

        public async Task<Order> GetOrderByID(int orderId)
        {
            return await _orderService.GetByID(orderId);
        }
    }
}
