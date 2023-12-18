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
    public class CartRepository : BaseRepository<Cart>, ICartRepository
    {
        public CartRepository(Context context)
        {
            _context = context;
        }

        public async Task<Cart> GetAsync(Guid id)
        {
            return await _context.CartItems
            .Include(c => c.ProduceType)
            .SingleOrDefaultAsync(a => a.Id == id && !a.IsDeleted);
        }

        public async Task<Cart> GetAsync(Expression<Func<Cart, bool>> expression)
        {
            return await _context.CartItems
            .Where(a => !a.IsDeleted)
            .Include(c => c.ProduceType)
            .SingleOrDefaultAsync(expression);
        }

       public async Task<IEnumerable<Cart>> GetAllAsync()
        {
            return await _context.CartItems.AsNoTracking()
           .Where(a => !a.IsDeleted)
           .Include(c => c.ProduceType)
           .ToListAsync();
        }

        public async Task<IEnumerable<Cart>>  GetSelectedAsync(List<Guid> ids)
        {
            return await _context.CartItems
            .Where(a => ids.Contains(a.Id) && !a.IsDeleted)
            .Include(c => c.ProduceType)
            .ToListAsync();
        }

        public async Task<IEnumerable<Cart>> GetSelectedAsync(Expression<Func<Cart, bool>> expression)
        {
            return await _context.CartItems
            // .Where(expression)
            .Include(c => c.ProduceType)
            .ToListAsync();
        }

        
    }
}