using CleanCar.DAL.Context;
using CleanCar.DAL.Models;
using CleanCar.DAL.Repositories.IRepositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Text;
using System.Threading.Tasks;

namespace CleanCar.DAL.Repositories
{
    public class OperationRepository : IOperationRepository
    {
        private readonly CleanCarContext _context;
        public OperationRepository(CleanCarContext context) => _context = context;

        public async Task<int> CreateOperationAsync(Operation operation)
        {
            _context.Operations.Add(operation);
            await _context.SaveChangesAsync();

            return operation.Id;
        }

        public async Task DeleteOperationrAsync(Operation operation)
        {
            _context.Entry(operation).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Operation>> GetOperationsAsync()
        {
            return await _context.Operations.ToListAsync();
        }

        public async Task<Operation> UpdateOperationAsync(Operation operation)
        {
            _context.Entry(operation).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return operation;
        }
    }
}