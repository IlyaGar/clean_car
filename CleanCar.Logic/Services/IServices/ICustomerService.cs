
using CleanCar.DAL.Models;
using CleanCar.Logic.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CleanCar.Logic.Services.IServices
{
    public interface ICustomerService
    {
        Task<Customer> GetCustomerAsync(int id);
        Task<IEnumerable<CustomerDTO>> GetCustomersAsync();
        Task<int> CreateCustomerAsync(Customer customer);
        Task<CustomerDTO> UpdateCustomerAsync(Customer customer);
        Task DeleteCustomerAsync(Customer customer);
    }
}
