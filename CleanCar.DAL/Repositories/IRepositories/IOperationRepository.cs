using CleanCar.DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CleanCar.DAL.Repositories.IRepositories
{
    public interface IOperationRepository
    {
        Task<IEnumerable<Operation>> GetOperationsAsync();
        Task<int> CreateOperationAsync(Operation operation);
        Task<Operation> UpdateOperationAsync(Operation operation);
        Task DeleteOperationrAsync(Operation operation);
    }
}
