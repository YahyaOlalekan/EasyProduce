using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Domain.Entity;

namespace Application.Abstractions.RepositoryInterfaces
{
    public interface IRequestRepository : IBaseRepository<Request>
    {
        Task<bool> CreateRequestAsync(Request request);
        Task<Request> GetRequestAsync(Expression<Func<Request, bool>> expression);
        Task<IEnumerable<Request>> GetSelectedAsync(List<Guid> ids);
        Task<IEnumerable<Request>> GetSelectedAsync(Expression<Func<Request, bool>> expression);
        Task<IEnumerable<Request>> GetAllAsync();
        Task<string> GenerateRequestNumAsync();

    }
}