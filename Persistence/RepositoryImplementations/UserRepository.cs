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
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(Context context)
        {
            _context = context;
        }
        public async Task<User> GetAsync(Guid id)
        {
            return await _context.Users
            .Include(a => a.Role)
            .Include(a => a.Farmer)
            .Where(a => !a.IsDeleted)
            .FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<User> GetAsync(Expression<Func<User, bool>> expression)
        {
            return await _context.Users
           .Include(a => a.Role)
           .Where(a => !a.IsDeleted)
           .FirstOrDefaultAsync(expression);
        }

        
        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _context.Users.AsNoTracking()
           .Include(a => a.Role)
           .Where(a => !a.IsDeleted)
           .ToListAsync();
        }

        public async Task<IEnumerable<User>> GetSelectedAsync(List<Guid> ids)
        {
            return await _context.Users
           .Include(a => a.Role)
           .Where(a => ids.Contains(a.Id) && !a.IsDeleted)
           .ToListAsync();
        }

        public async Task<IEnumerable<User>> GetSelectedAsync(Expression<Func<User, bool>> expression)
        {
            return await _context.Users
           .Include(a => a.Role)
           .Where(expression)
           .ToListAsync();
        }

      
    }
}