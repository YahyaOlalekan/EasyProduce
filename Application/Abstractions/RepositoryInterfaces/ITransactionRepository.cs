using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Domain.Entity;

namespace Application.Abstractions.RepositoryInterfaces
{
    public interface ITransactionRepository : IBaseRepository<Transaction>
    {
        Task<bool> CreateTransactionAsync(Transaction transaction);
        Task<bool> CreateTransactionsAsync(List<Transaction> transactions);
        Task<Transaction> GetAsync(Guid id);
        Task<Transaction> GetAsync(Expression<Func<Transaction, bool>> expression);
        Task<IEnumerable<Transaction>> GetSelectedAsync(List<Guid> ids);
        Task<IEnumerable<Transaction>> GetSelectedAsync(Expression<Func<Transaction, bool>> expression);
        Task<IEnumerable<Transaction>> GetAllAsync();
        Task<IEnumerable<Transaction>> GetAllInitiatedProducetypeSalesAsync();
        Task<IEnumerable<Transaction>> GetAllConfirmedProducetypeSalesAsync();
        Task<string> GenerateTransactionRegNumAsync();
    }
}