using CleanCar.DAL.Context;
using CleanCar.DAL.Models;
using CleanCar.DAL.Repositories.IRepositories;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanCar.DAL.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly CleanCarContext _context;
        public OrderRepository(CleanCarContext context) => _context = context;

        public async Task<int> CreateOrderAsync(Order order)
        {
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            return order.Id;
        }

        public async Task CreateListOrdersAsync(IEnumerable<Order> orders)
        {
             _context.Orders.AddRange(orders);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteOrderAsync(Order order)
        {
            _context.Entry(order).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Order>> GetOrdersAsync(int id)
        {
            var customerOperations = await _context.Orders.Where(c => c.CustomerId == id).Include(c => c.Operation).ToListAsync();

            return customerOperations;
        }
    }
}