using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Domain.Entity;

namespace Application.Abstractions.RepositoryInterfaces
{
    public interface IManagerRepository : IBaseRepository<Manager>
    {
        Task<Manager> GetAsync(Guid id);
         Task<Manager> GetManagerByManagerIdAsync(Guid id);
        Task<Manager> GetAsync(Expression<Func<Manager, bool>> expression);
        Task<IEnumerable<Manager>> GetSelectedAsync(List<Guid> ids);
        Task<IEnumerable<Manager>> GetSelectedAsync(Expression<Func<Manager, bool>> expression);
        Task<IEnumerable<Manager>> GetAllAsync();
    }
}