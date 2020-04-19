using AutoMapper;
using CleanCar.DAL.Models;
using CleanCar.DAL.Repositories.IRepositories;
using CleanCar.Logic.Models;
using CleanCar.Logic.Services.IServices;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CleanCar.Logic.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _repository;
        private readonly IMapper _mapper;

        public OrderService(IOrderRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<int> CreateOrderAsync(Order customerOperation)
        {
            return await _repository.CreateOrderAsync(customerOperation);
        }

        public async Task CreateListOrderAsync(IEnumerable<Order> customerOperations)
        {
            await _repository.CreateListOrdersAsync(customerOperations);
        }

        public async Task DeleteOrderAsync(Order customerOperation)
        {
            await _repository.DeleteOrderAsync(customerOperation);
        }

        public async Task<IEnumerable<OrderDTO>> GetOrderDTOsByCustomerIdAsync(int id)
        {
            var customerOperations = await _repository.GetOrdersAsync(id);

            var customerOperationDTOs = _mapper.Map<IEnumerable<OrderDTO>>(customerOperations);

            return customerOperationDTOs;
        }
    }
}