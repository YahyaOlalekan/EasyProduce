using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Domain.Entity;

namespace Application.Abstractions.RepositoryInterfaces
{
    public interface IOrderRepository : IBaseRepository<Order>
    {
        Task<bool> CreateOrderAsync(List<Order> orders);
        Task<Order> GetAsync(Guid id);
        Task<Order> GetAsync(Expression<Func<Order, bool>> expression);
        Task<IEnumerable<Order>> GetSelectedAsync(List<Guid> ids);
        Task<IEnumerable<Order>> GetSelectedAsync(Expression<Func<Order, bool>> expression);
        Task<IEnumerable<Order>> GetAllAsync();
        Task<string> GenerateOrderNumberAsync();
    }
}