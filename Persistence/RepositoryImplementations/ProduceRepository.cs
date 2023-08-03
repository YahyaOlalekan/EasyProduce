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
    public class ProduceRepository : BaseRepository<Produce>, IProduceRepository
    {
        public ProduceRepository(Context context)
        {
            _context = context;
        }

        public async Task<Produce> GetAsync(Guid id)
        {
            return await _context.Produces
            .Where(a => !a.IsDeleted)
            .Include(a => a.Category)
             .SingleOrDefaultAsync(x => x.Id == id);
        }

       
        public async Task<Produce> GetAsync(Expression<Func<Produce, bool>> expression)
        {
            return await _context.Produces
            .Where(a => !a.IsDeleted)
            .Include(a => a.Category)
            .SingleOrDefaultAsync(expression);
        }


        public async Task<IEnumerable<Produce>> GetAllAsync()
        {
            return await _context.Produces.AsNoTracking()
            .Where(a => !a.IsDeleted)
            .Include(a => a.Category)
            .ToListAsync();
        }

      
        public async Task<IEnumerable<Produce>> GetSelectedAsync(Expression<Func<Produce, bool>> expression)
        {
            return await _context.Produces
            .Include(a => a.Category)
            .Where(expression)
            .ToListAsync();
        }

        public async Task<IEnumerable<Produce>> GetSelectedAsync(List<Guid> ids)
        {
            return await _context.Produces
            .Include(a => a.Category)
            .Where(a => ids.Contains(a.Id) && !a.IsDeleted)
            .ToListAsync();
        }

       
    }
}