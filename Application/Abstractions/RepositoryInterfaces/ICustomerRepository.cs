using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Domain.Entity;

namespace Application.Abstractions.RepositoryInterfaces
{
    public interface ICustomerRepository : IBaseRepository<Customer>
    {
        Task<Customer> GetAsync(Guid id);
        Task<Customer> GetAsync(Expression<Func<Customer, bool>> expression);
        Task<IEnumerable<Customer>> GetSelectedAsync(List<Guid> ids);
        Task<IEnumerable<Customer>> GetSelectedAsync(Expression<Func<Customer, bool>> expression);
        Task<IEnumerable<Customer>> GetAllAsync(Expression<Func<Customer, bool>> expression);
        Task<IEnumerable<Customer>> GetAllAsync();
    }
}
