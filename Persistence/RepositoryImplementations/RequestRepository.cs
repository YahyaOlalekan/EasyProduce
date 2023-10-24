using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Application.Abstractions.RepositoryInterfaces;
using Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Persistence.AppDbContext;


namespace Persistence.RepositoryImplementations
{
    public class RequestRepository : BaseRepository<Request>, IRequestRepository
    {
        public RequestRepository(Context context)
        {
            _context = context;
        }

        public async Task<bool> CreateRequestAsync(Request request)
        {
           await _context.AddAsync(request);
            return true;
        }
       

        public async Task<Request> GetRequestAsync(Guid id)
        {
            return await _context.Requests
            .Include(a => a.Farmer)
            // .ThenInclude(a => a.ProduceType)
            .SingleOrDefaultAsync(a => a.Id == id && !a.IsDeleted);
        }

        public async Task<Request> GetRequestAsync(Expression<Func<Request, bool>> expression)
        {
            return await _context.Requests
            .Where(a => !a.IsDeleted)
            .Include(a => a.Farmer)
            // .ThenInclude(a => a.ProduceType)
            .SingleOrDefaultAsync(expression);
        }

        public async Task<IEnumerable<Request>> GetAllAsync()
        {
            return await _context.Requests.AsNoTracking()
           .Where(a => !a.IsDeleted)
            .Include(a => a.Farmer)
            // .ThenInclude(a => a.ProduceType)
           .ToListAsync();
        }

        public async Task<IEnumerable<Request>> GetSelectedAsync(List<Guid> ids)
        {
            return await _context.Requests
            .Where(a => ids.Contains(a.Id) && !a.IsDeleted)
            .Include(a => a.Farmer)
            // .ThenInclude(a => a.ProduceType)
            .ToListAsync();
        }

        public async Task<IEnumerable<Request>> GetSelectedAsync(Expression<Func<Request, bool>> expression)
        {
            return await _context.Requests
            .Where(expression)
            .Include(a => a.Farmer)
            .ThenInclude(a => a.FarmerProduceTypes)
            .ThenInclude(x=>x.ProduceType)
            .ToListAsync();
        }

        public async Task<string> GenerateRequestNumAsync()
        {
            var count = await GetAllAsync();
            return "EP/REQ/" + $"{count.Count() + 1}";
        }

      

      
    }
}