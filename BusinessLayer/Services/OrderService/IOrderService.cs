using DataAccessLayer.Repositors.Base;
using EDIG_Api_Task.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services.OrderService
{
    public interface IOrderService 
    {
        Task<IEnumerable<Order>> GetAllOrders(string[]? includes = null);
        Task AddOrder(Order order);
        Task<Order> UpdateOrder(Order order);
        Task DeleteOrder(int id);
        Task<Order>GetOrderByID(int orderId);
    }
}
