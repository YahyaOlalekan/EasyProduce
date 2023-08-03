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
    public class TransactionRepository : BaseRepository<Transaction>, ITransactionRepository
    {
        public TransactionRepository(Context context)
        {
            _context = context;
        }

        public async Task<bool> CreateTransactionsAsync(List<Transaction> transactions)
        {
            _context.AddRangeAsync(transactions);
            return true;
        }

        public async Task<Transaction> GetAsync(Guid id)
        {
            return await _context.Transactions
            .Include(a => a.Farmer)
            .Include(a => a.TransactionProduceTypes)
            .ThenInclude(a => a.ProduceType)
            .SingleOrDefaultAsync(a => a.Id == id && !a.IsDeleted);
        }

        public async Task<Transaction> GetAsync(Expression<Func<Transaction, bool>> expression)
        {
            return await _context.Transactions
            .Where(a => !a.IsDeleted)
            .Include(a => a.Farmer)
            .Include(a => a.TransactionProduceTypes)
            .ThenInclude(a => a.ProduceType)
            .SingleOrDefaultAsync(expression);
        }

        public async Task<IEnumerable<Transaction>> GetAllAsync()
        {
            return await _context.Transactions.AsNoTracking()
           .Where(a => !a.IsDeleted)
            .Include(a => a.Farmer)
            .Include(a => a.TransactionProduceTypes)
            .ThenInclude(a => a.ProduceType)
           .ToListAsync();
        }

        public async Task<IEnumerable<Transaction>> GetSelectedAsync(List<Guid> ids)
        {
            return await _context.Transactions
            .Where(a => ids.Contains(a.Id) && !a.IsDeleted)
            .Include(a => a.Farmer)
            .Include(a => a.TransactionProduceTypes)
            .ThenInclude(a => a.ProduceType)
            .ToListAsync();
        }

        public async Task<IEnumerable<Transaction>> GetSelectedAsync(Expression<Func<Transaction, bool>> expression)
        {
            return await _context.Transactions
            .Where(expression)
            .Include(a => a.Farmer)
            .Include(a => a.TransactionProduceTypes)
            .ThenInclude(a => a.ProduceType)
            .ToListAsync();
        }

        public async Task<string> GenerateTransactionRegNumAsync()
        {
            var count = await GetAllAsync();
            return "EP/TRA/00" + $"{count.Count() + 1}";
        }

    }
}