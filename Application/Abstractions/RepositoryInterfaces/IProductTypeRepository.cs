using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Domain.Entity;

namespace Application.Abstractions.RepositoryInterfaces
{
    public interface IProductTypeRepository : IBaseRepository<ProductType>
    {
        Task<ProductType> GetAsync(Guid id);
        Task<ProductType> GetAsync(Expression<Func<ProductType, bool>> expression);
        Task<IEnumerable<ProductType>> GetSelectedAsync(List<Guid> ids);
        Task<IEnumerable<ProductType>> GetSelectedAsync(Expression<Func<ProductType, bool>> expression);
        Task<IEnumerable<ProductType>> GetAllAsync();
    }
}