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
    public class RoleRepository : BaseRepository<Role>, IRoleRepository
    {
        public RoleRepository(Context context)
        {
            _context = context;
        }

        public async Task<Role> GetAsync(Guid id)
        {
            return await _context.Roles
            .SingleOrDefaultAsync(a => a.Id == id && !a.IsDeleted);
        }

        public async Task<Role> GetAsync(Expression<Func<Role, bool>> expression)
        {
            return await _context.Roles
            .Where(a => !a.IsDeleted)
            .SingleOrDefaultAsync(expression);
        }

      
        public async Task<IEnumerable<Role>> GetAllAsync()
        {
            return await _context.Roles.AsNoTracking()
           .Where(a => !a.IsDeleted)
           .ToListAsync();
        }

        public async Task<IEnumerable<Role>> GetSelectedAsync(List<Guid> ids)
        {
            return await _context.Roles
            .Where(a => ids.Contains(a.Id) && !a.IsDeleted)
            .ToListAsync();
        }

        public async Task<IEnumerable<Role>> GetSelectedAsync(Expression<Func<Role, bool>> expression)
        {
            return await _context.Roles
            .Where(expression)
            .ToListAsync();
        }

        
    }
}