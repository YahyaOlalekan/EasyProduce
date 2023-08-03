using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Domain.Entity;

namespace Application.Abstractions.RepositoryInterfaces
{
    public interface IProduceRepository : IBaseRepository<Produce>
    {
        Task<Produce> GetAsync(Guid id);
        Task<Produce> GetAsync(Expression<Func<Produce, bool>> expression);
        Task<IEnumerable<Produce>> GetSelectedAsync(List<Guid> ids);
        Task<IEnumerable<Produce>> GetSelectedAsync(Expression<Func<Produce, bool>> expression);
        Task<IEnumerable<Produce>> GetAllAsync();
    }
}
