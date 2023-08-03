using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Domain.Entity;

namespace Application.Abstractions.RepositoryInterfaces
{
    public interface IFarmerRepository : IBaseRepository<Farmer>
    {
        Task<Farmer> GetAsync(Guid id);
        Task<Farmer> GetAsync(Expression<Func<Farmer, bool>> expression);
        Task<IEnumerable<Farmer>> GetSelectedAsync(List<Guid> ids);
        Task<IEnumerable<Farmer>> GetSelectedAsync(Expression<Func<Farmer, bool>> expression);
        Task<IEnumerable<Farmer>> GetAllAsync(Expression<Func<Farmer, bool>> expression);
        Task<IEnumerable<Farmer>> GetAllAsync();
    }
}