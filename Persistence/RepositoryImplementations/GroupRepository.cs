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
    public class GroupRepository : BaseRepository<Group>, IGroupRepository
    {
        public GroupRepository(Context context)
        {
            _context = context;
        }

        public async Task<Group> GetAsync(Guid id)
        {
            return await _context.Groups
            .SingleOrDefaultAsync(a => a.Id == id && !a.IsDeleted);
        }

        public async Task<Group> GetAsync(Expression<Func<Group, bool>> expression)
        {
            return await _context.Groups
            .Where(a => !a.IsDeleted )
            .SingleOrDefaultAsync(expression);
        }

        public async Task<IEnumerable<Group>> GetAllAsync()
        {
            return await _context.Groups.AsNoTracking()
           .Where(a => !a.IsDeleted )
           .ToListAsync();
        }

        public async Task<IEnumerable<Group>> GetSelectedAsync(List<Guid> ids)
        {
            return await _context.Groups
            .Where(a => ids.Contains(a.Id) && !a.IsDeleted )
            .ToListAsync();
        }

        public async Task<IEnumerable<Group>> GetSelectedAsync(Expression<Func<Group, bool>> expression)
        {
            return await _context.Groups
            .Where(expression)
            .ToListAsync();
        }

       
    }
}