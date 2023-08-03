using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Domain.Entity;

namespace Application.Abstractions.RepositoryInterfaces
{
    public interface ICartItemForOrderRepository : IBaseRepository<CartItemForOrder>
    {
        Task<CartItemForOrder> GetAsync(Guid id);
        Task<CartItemForOrder> GetAsync(Expression<Func<CartItemForOrder, bool>> expression);
        Task<IEnumerable<CartItemForOrder>> GetSelectedAsync(List<Guid> ids);
        Task<IEnumerable<CartItemForOrder>> GetSelectedAsync(Expression<Func<CartItemForOrder, bool>> expression);
        Task<IEnumerable<CartItemForOrder>> GetAllAsync();
    }
}