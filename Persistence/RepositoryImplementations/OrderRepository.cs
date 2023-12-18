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
    public class OrderRepository : BaseRepository<Order>, IOrderRepository
    {
        public OrderRepository(Context context)
        {
            _context = context;
        }

        public async Task<bool> CreateOrderAsync(List<Order> orders)
        {
            await _context.AddRangeAsync(orders);
            return true;
        }

        public async Task<Order> GetAsync(Guid id)
        {
            return await _context.Orders
            .Include(a => a.Customer)
            .Include(a => a.OrderProductTypes)
            .ThenInclude(a => a.ProductType)
            // .ThenInclude(a =>a.Produce)
            .SingleOrDefaultAsync(a => a.Id == id && !a.IsDeleted);
        }

        public async Task<Order> GetAsync(Expression<Func<Order, bool>> expression)
        {
            return await _context.Orders
            .Where(a => !a.IsDeleted)
            .Include(a => a.Customer)
            .Include(a => a.OrderProductTypes)
            .ThenInclude(a => a.ProductType)
            // .ThenInclude(a =>a.Produce)
            .SingleOrDefaultAsync(expression);
        }

        public async Task<IEnumerable<Order>> GetAllAsync()
        {
            return await _context.Orders.AsNoTracking()
           .Where(a => !a.IsDeleted)
            .Include(a => a.Customer)
            .Include(a => a.OrderProductTypes)
            .ThenInclude(a => a.ProductType)
            // .ThenInclude(a =>a.Produce)
           .ToListAsync();
        }

        public async Task<IEnumerable<Order>> GetSelectedAsync(List<Guid> ids)
        {
            return await _context.Orders
            .Where(a => ids.Contains(a.Id) && !a.IsDeleted)
            .Include(a => a.Customer)
            .Include(a => a.OrderProductTypes)
            .ThenInclude(a => a.ProductType)
            // .ThenInclude(a =>a.Produce)
            .ToListAsync();
        }

        public async Task<IEnumerable<Order>> GetSelectedAsync(Expression<Func<Order, bool>> expression)
        {
            return await _context.Orders
            .Where(expression)
            .Include(a => a.Customer)
            .Include(a => a.OrderProductTypes)
            .ThenInclude(a => a.ProductType)
            // .ThenInclude(a =>a.Produce)
            .ToListAsync();
        }

        public async Task<string> GenerateOrderNumberAsync()
        {
            var  count = await GetAllAsync();
            return "EP/ODR/00" + $"{count.Count() + 1}";
        }

       
    }
}