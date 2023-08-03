using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Domain.Entity;

namespace Application.Abstractions.RepositoryInterfaces
{
    public interface IProduceTypeRepository : IBaseRepository<ProduceType>
    {
        Task<ProduceType> GetAsync(Guid id);
        Task<ProduceType> GetAsync(Expression<Func<ProduceType, bool>> expression);
        Task<IEnumerable<ProduceType>> GetSelectedAsync(List<Guid> ids);
        Task<IEnumerable<ProduceType>> GetSelectedAsync(Expression<Func<ProduceType, bool>> expression);
        Task<IEnumerable<ProduceType>> GetAllAsync();
    }
}