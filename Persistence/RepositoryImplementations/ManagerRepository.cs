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
    public class ManagerRepository : BaseRepository<Manager>, IManagerRepository
    {
        public ManagerRepository(Context context)
        {
            _context = context;
        }

        public async Task<Manager> GetAsync(Guid id)
        {
            return await _context.Managers
           .Include(a => a.User)
           .SingleOrDefaultAsync(a => a.UserId == id && !a.IsDeleted);
        }

        public async Task<Manager> GetAsync(Expression<Func<Manager, bool>> expression)
        {
            var result = await _context.Managers
           .Where(a => !a.IsDeleted )
            .Include(a => a.User)
            .SingleOrDefaultAsync(expression);

            return result;
        }

        public async Task<IEnumerable<Manager>> GetAllAsync()
        {
            return await _context.Managers.AsNoTracking()
            .Where(a => !a.IsDeleted)
            .Include(a => a.User)
            .ToListAsync();
        }

        public async Task<IEnumerable<Manager>> GetSelectedAsync(List<Guid> ids)
        {
            return await _context.Managers
            .Include(a => a.User)
            .Where(a => !a.IsDeleted)
            .ToListAsync();
        }


        public async Task<IEnumerable<Manager>> GetSelectedAsync(Expression<Func<Manager, bool>> expression)
        {
            return await _context.Managers
            .Where(expression)
            .ToListAsync();
        }

    }
}