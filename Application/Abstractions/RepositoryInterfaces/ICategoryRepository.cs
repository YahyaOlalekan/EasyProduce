using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Domain.Entity;

namespace Application.Abstractions.RepositoryInterfaces
{
    public interface ICategoryRepository : IBaseRepository<Category>
    {
        Task<Category> GetAsync(Guid id);
        Task<Category> GetAsync(Expression<Func<Category, bool>> expression);
        Task<IEnumerable<Category>> GetSelectedAsync(List<Guid> ids);
        Task<IEnumerable<Category>> GetSelectedAsync(Expression<Func<Category, bool>> expression);
        Task<IEnumerable<Category>> GetAllAsync();
    }
}
