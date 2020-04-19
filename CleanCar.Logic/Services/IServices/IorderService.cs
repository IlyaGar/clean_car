using CleanCar.DAL.Models;
using CleanCar.Logic.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CleanCar.Logic.Services.IServices
{
    public interface IOrderService
    {
        Task<IEnumerable<OrderDTO>> GetOrderDTOsByCustomerIdAsync(int id);
        Task<int> CreateOrderAsync(Order customerOperation);
        Task DeleteOrderAsync(Order customerOperation);
        Task CreateListOrderAsync(IEnumerable<Order> customerOperations);
    }
}
