using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Domain.Entity;

namespace Application.Abstractions.RepositoryInterfaces
{
    public interface IOrderProductTypeRepository : IBaseRepository<OrderProductType>
    {
        Task<bool> CreateOrderProductTypeAsync(List<OrderProductType> orders);
        Task<OrderProductType> GetAsync(Guid id);
        Task<OrderProductType> GetAsync(Expression<Func<OrderProductType, bool>> expression);
        Task<IEnumerable<OrderProductType>> GetSelectedAsync(List<Guid> ids);
        Task<IEnumerable<OrderProductType>> GetSelectedAsync(Expression<Func<OrderProductType, bool>> expression);
        Task<IEnumerable<OrderProductType>> GetAllAsync();
    }
}