
using CleanCar.DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CleanCar.Logic.Services.IServices
{
    public interface IOperationService
    {
        Task<IEnumerable<Operation>> GetOperationsAsync();
        Task<int> CreateOperationAsync(Operation operation);
        Task<Operation> UpdateOperationAsync(Operation operation);
        Task DeleteOperationAsync(Operation operation);
    }
}
