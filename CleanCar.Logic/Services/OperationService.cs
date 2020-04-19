using CleanCar.DAL.Models;
using CleanCar.DAL.Repositories.IRepositories;
using CleanCar.Logic.Services.IServices;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CleanCar.Logic.Services
{
    public class OperationService : IOperationService
    {
        private readonly IOperationRepository _repository;

        public OperationService(IOperationRepository repository) => _repository = repository;

        public async Task<int> CreateOperationAsync(Operation operation)
        {
            return await _repository.CreateOperationAsync(operation);
        }

        public async Task DeleteOperationAsync(Operation operation)
        {
            await _repository.DeleteOperationrAsync(operation);
        }

        public async Task<IEnumerable<Operation>> GetOperationsAsync()
        {
            return await _repository.GetOperationsAsync();
        }

        public async Task<Operation> UpdateOperationAsync(Operation operation)
        {
            return await _repository.UpdateOperationAsync(operation);
        }
    }
}