using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Domain.Entity;

namespace Application.Abstractions.RepositoryInterfaces
{
    public interface IRoleRepository : IBaseRepository<Role>
    {
        Task<Role> GetAsync(Guid id);
        Task<Role> GetAsync(Expression<Func<Role, bool>> expression);
        Task<IEnumerable<Role>> GetSelectedAsync(List<Guid> ids);
        Task<IEnumerable<Role>> GetSelectedAsync(Expression<Func<Role, bool>> expression);
        Task<IEnumerable<Role>> GetAllAsync();
    }
}