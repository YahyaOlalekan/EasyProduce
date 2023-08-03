using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Domain.Entity;

namespace Application.Abstractions.RepositoryInterfaces
{
    public interface ICartItemRepository : IBaseRepository<CartItem>
    {
        Task<CartItem> GetAsync(Guid id);
        Task<CartItem> GetAsync(Expression<Func<CartItem, bool>> expression);
        Task<IEnumerable<CartItem>> GetSelectedAsync(List<Guid> ids);
        Task<IEnumerable<CartItem>> GetSelectedAsync(Expression<Func<CartItem, bool>> expression);
        Task<IEnumerable<CartItem>> GetAllAsync();
    }
}