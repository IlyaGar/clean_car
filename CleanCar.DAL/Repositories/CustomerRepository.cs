using CleanCar.DAL.Context;
using CleanCar.DAL.Models;
using CleanCar.DAL.Repositories.IRepositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanCar.DAL.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly CleanCarContext _context;
        public CustomerRepository(CleanCarContext context) => _context = context;

        public async Task<int> CreateCustomerAsync(Customer customer)
        {
            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();

            return customer.Id;
        }

        public async Task DeleteCustomerAsync(Customer customer)
        {
            _context.Entry(customer).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
        }

        public async Task<Customer> GetCustomerAsync(int id)
        {
            IQueryable<Customer> customers = _context.Customers.Include(c => c.Orders);
            var r = await customers.Include(c => c.Orders).FirstOrDefaultAsync(c => c.Id == id);
            return r;
        }

        public async Task<IEnumerable<Customer>> GetCustomersAsync()
        {
            return await _context.Customers.Include(c => c.Orders).ToListAsync();
        }

        public async Task<Customer> UpdateCustomerAsync(Customer customer)
        {
            _context.Entry(customer).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return customer;
        }
    }
}
