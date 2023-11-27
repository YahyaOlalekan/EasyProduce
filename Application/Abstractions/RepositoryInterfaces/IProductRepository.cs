using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Domain.Entity;

namespace Application.Abstractions.RepositoryInterfaces
{
    public interface IProductRepository : IBaseRepository<Product>
    {
        // Task<Product> GetAsync(Guid id);
        // Task<Product> GetAsync(Expression<Func<Product, bool>> expression);
        // Task<IEnumerable<Product>> GetSelectedAsync(List<Guid> ids);
        // // Task<IEnumerable<Product>> GetSelectedAsync(Expression<Func<Product, bool>> expression);
        // Task<IEnumerable<Product>> GetAllAsync();
    }
}
