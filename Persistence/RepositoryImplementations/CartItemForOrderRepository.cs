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
    public class CartItemForOrderRepository : BaseRepository<CartItemForOrder>, ICartItemForOrderRepository
    {
        public CartItemForOrderRepository(Context context)
        {
            _context = context;
        }

        public async Task<CartItemForOrder> GetAsync(Guid id)
        {
           return await _context.CartItemForOrders
            .Include(c => c.ProductType)
            // .ThenInclude(c => c.Produce)
            .SingleOrDefaultAsync(a => a.Id == id && !a.IsDeleted);
        }

        public async Task<CartItemForOrder> GetAsync(Expression<Func<CartItemForOrder, bool>> expression)
        {
            return await _context.CartItemForOrders
            .Where(a => !a.IsDeleted)
            .Include(c => c.ProductType)
            // .ThenInclude(c => c.Produce)
            .SingleOrDefaultAsync(expression);
        }


        public async Task<IEnumerable<CartItemForOrder>> GetAllAsync()
        {
            return await _context.CartItemForOrders.AsNoTracking()
           .Where(a => !a.IsDeleted)
           .Include(c => c.ProductType)
            //.ThenInclude(c => c.Produce)
           .ToListAsync();
        }

        public async Task<IEnumerable<CartItemForOrder>> GetSelectedAsync(List<Guid> ids)
        {
            return await _context.CartItemForOrders
            .Where(a => ids.Contains(a.Id) && !a.IsDeleted)
            .Include(c => c.ProductType)
            //.ThenInclude(c => c.Produce)
            .ToListAsync();
        }

        public async Task<IEnumerable<CartItemForOrder>> GetSelectedAsync(Expression<Func<CartItemForOrder, bool>> expression)
        {
            return await _context.CartItemForOrders
            .Where(expression)
            .Include(c => c.ProductType)
            // .ThenInclude(c => c.Produce)
            .ToListAsync();
        }

    }
}