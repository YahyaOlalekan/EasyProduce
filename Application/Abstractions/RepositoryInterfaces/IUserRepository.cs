using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Domain.Entity;

namespace Application.Abstractions.RepositoryInterfaces
{
    public interface IUserRepository : IBaseRepository<User>
    {
        Task<User> GetAsync(Guid id);
        Task<User> GetAsync(Expression<Func<User, bool>> expression);
        Task<IEnumerable<User>> GetSelectedAsync(List<Guid> ids);
        Task<IEnumerable<User>> GetSelectedAsync(Expression<Func<User, bool>> expression);
        Task<IEnumerable<User>> GetAllAsync();
    }
}