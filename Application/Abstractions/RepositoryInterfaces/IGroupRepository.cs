using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Domain.Entity;

namespace Application.Abstractions.RepositoryInterfaces
{
    public interface IGroupRepository : IBaseRepository<Group>
    {
        Task<Group> GetAsync(Guid id);
        Task<Group> GetAsync(Expression<Func<Group, bool>> expression);
        Task<IEnumerable<Group>> GetSelectedAsync(List<Guid> ids);
        Task<IEnumerable<Group>> GetSelectedAsync(Expression<Func<Group, bool>> expression);
        Task<IEnumerable<Group>> GetAllAsync();
    }
}
