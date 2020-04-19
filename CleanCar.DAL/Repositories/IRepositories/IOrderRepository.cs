using CleanCar.DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CleanCar.DAL.Repositories.IRepositories
{
    public interface IOrderRepository
    {
        Task<IEnumerable<Order>> GetOrdersAsync(int id);
        Task<int> CreateOrderAsync(Order order);
        Task CreateListOrdersAsync(IEnumerable<Order> orders);
        Task DeleteOrderAsync(Order order);
    }
}
