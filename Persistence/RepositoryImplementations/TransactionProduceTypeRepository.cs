using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Application.Abstractions.RepositoryInterfaces;
using Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Persistence.AppDbContext;


namespace Persistence.RepositoryImplementations
{
    public class TransactionProduceTypeRepository : BaseRepository<TransactionProduceType>, ITransactionProduceTypeRepository
    {
        public TransactionProduceTypeRepository(Context context)
        {
            _context = context;
        }

        public async Task<bool> CreateTransactionProduceTypeAsync(List<TransactionProduceType> transactions)
        {
            await  _context.AddRangeAsync(transactions);
            return true;
        }

        public async Task<TransactionProduceType> GetAsync(Guid id)
        {
            return await _context.TransactionProduceTypes
            .Include(a => a.Transaction)
            .Include(a => a.ProduceType)
            .SingleOrDefaultAsync(a => a.Id == id && !a.IsDeleted);
        }

        public async Task<TransactionProduceType> GetAsync(Expression<Func<TransactionProduceType, bool>> expression)
        {
            return await _context.TransactionProduceTypes
            .Where(a => !a.IsDeleted)
            .Include(a => a.Transaction)
            .Include(a => a.ProduceType)
            .SingleOrDefaultAsync(expression);
        }

        public async Task<IEnumerable<TransactionProduceType>> GetAllAsync()
        {
            return await _context.TransactionProduceTypes.AsNoTracking()
           .Where(a => !a.IsDeleted)
           .ToListAsync();
        }

        public async Task<IEnumerable<TransactionProduceType>> GetSelectedAsync(List<Guid> ids)
        {
            return await _context.TransactionProduceTypes
            .Where(a => ids.Contains(a.Id) && !a.IsDeleted)
            .Include(a => a.Transaction)
            .Include(a => a.ProduceType)
            .ToListAsync();
        }

        public async Task<IEnumerable<TransactionProduceType>> GetSelectedAsync(Expression<Func<TransactionProduceType, bool>> expression)
        {
            return await _context.TransactionProduceTypes
            .Where(expression)
            .Include(a => a.Transaction)
            .Include(a => a.ProduceType)
            .ToListAsync();
        }

    }
}