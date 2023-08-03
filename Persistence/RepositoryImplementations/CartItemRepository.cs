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
    public class CartItemRepository : BaseRepository<CartItem>, ICartItemRepository
    {
        public CartItemRepository(Context context)
        {
            _context = context;
        }

        public async Task<CartItem> GetAsync(Guid id)
        {
            return await _context.CartItems
            .Include(c => c.ProduceType)
            .SingleOrDefaultAsync(a => a.Id == id && !a.IsDeleted);
        }

        public async Task<CartItem> GetAsync(Expression<Func<CartItem, bool>> expression)
        {
            return await _context.CartItems
            .Where(a => !a.IsDeleted)
            .Include(c => c.ProduceType)
            .SingleOrDefaultAsync(expression);
        }

       public async Task<IEnumerable<CartItem>> GetAllAsync()
        {
            return await _context.CartItems.AsNoTracking()
           .Where(a => !a.IsDeleted)
           .Include(c => c.ProduceType)
           .ToListAsync();
        }

        public async Task<IEnumerable<CartItem>>  GetSelectedAsync(List<Guid> ids)
        {
            return await _context.CartItems
            .Where(a => ids.Contains(a.Id) && !a.IsDeleted)
            .Include(c => c.ProduceType)
            .ToListAsync();
        }

        public async Task<IEnumerable<CartItem>> GetSelectedAsync(Expression<Func<CartItem, bool>> expression)
        {
            return await _context.CartItems
            .Where(expression)
            .Include(c => c.ProduceType)
            .ToListAsync();
        }

        
    }
}