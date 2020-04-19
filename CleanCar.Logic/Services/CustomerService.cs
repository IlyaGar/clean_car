using AutoMapper;
using CleanCar.DAL.Models;
using CleanCar.DAL.Repositories.IRepositories;
using CleanCar.Logic.Models;
using CleanCar.Logic.Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanCar.Logic.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _repository;
        private readonly IMapper _mapper;       

        public CustomerService(ICustomerRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<int> CreateCustomerAsync(Customer customer)
        {
            customer.CreateDate = DateTime.Now;
            return await _repository.CreateCustomerAsync(customer);
        }

        public async Task DeleteCustomerAsync(Customer customer)
        {
            await _repository.DeleteCustomerAsync(customer);
        }

        public async Task<Customer> GetCustomerAsync(int id)
        {
            return await _repository.GetCustomerAsync(id); ;
        }

        public async Task<IEnumerable<CustomerDTO>> GetCustomersAsync()
        { 
            var customers = await _repository.GetCustomersAsync();

            var customerDTOs = _mapper.Map<IEnumerable<CustomerDTO>>(customers);

            return customerDTOs;
        }

        public async Task<CustomerDTO> UpdateCustomerAsync(Customer customer)
        {
            return _mapper.Map<CustomerDTO>(await _repository.UpdateCustomerAsync(customer));
        }
    }
}
