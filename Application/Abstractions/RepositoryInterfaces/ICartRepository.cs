using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Domain.Entity;

namespace Application.Abstractions.RepositoryInterfaces
{
    public interface ICartRepository : IBaseRepository<Cart>
    {
        Task<Cart> GetAsync(Guid id);
        Task<Cart> GetAsync(Expression<Func<Cart, bool>> expression);
        Task<IEnumerable<Cart>> GetSelectedAsync(List<Guid> ids);
        Task<IEnumerable<Cart>> GetSelectedAsync(Expression<Func<Cart, bool>> expression);
        Task<IEnumerable<Cart>> GetAllAsync();
    }
}