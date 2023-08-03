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
    public class OrderProductTypeRepository : BaseRepository<OrderProductType>, IOrderProductTypeRepository
    {
        public OrderProductTypeRepository(Context context)
        {
            _context = context;
        }


        public async Task<bool> CreateOrderProductTypeAsync(List<OrderProductType> OrderProductTypes)
        {
            await _context.AddRangeAsync(OrderProductTypes);
            return true;
        }

        public async Task<OrderProductType> GetAsync(Guid id)
        {
            return await _context.OrderProductTypes
            .Include(a => a.Order)
            .Include(a => a.ProductType)
            .SingleOrDefaultAsync(a => a.Id == id && !a.IsDeleted);
        }

        public async Task<OrderProductType> GetAsync(Expression<Func<OrderProductType, bool>> expression)
        {
            return await _context.OrderProductTypes
            .Where(a => !a.IsDeleted)
            .Include(a => a.Order)
            .Include(a => a.ProductType)
            //  .ThenInclude(a =>a.Produce)
            .SingleOrDefaultAsync(expression);
        }

        public async Task<IEnumerable<OrderProductType>> GetAllAsync()
        {
            return await _context.OrderProductTypes.AsNoTracking()
           .Where(a => !a.IsDeleted)
           .ToListAsync();
        }

        public async Task<IEnumerable<OrderProductType>> GetSelectedAsync(List<Guid> ids)
        {
            return await _context.OrderProductTypes
            .Where(a => ids.Contains(a.Id) && !a.IsDeleted)
            .Include(a => a.Order)
            .Include(a => a.ProductType)
            //.ThenInclude(a =>a.Produce)
            .ToListAsync();
        }

        public async Task<IEnumerable<OrderProductType>> GetSelectedAsync(Expression<Func<OrderProductType, bool>> expression)
        {
            return await _context.OrderProductTypes
            .Where(expression)
            .Include(a => a.Order)
            .Include(a => a.ProductType)
            //.ThenInclude(a =>a.Produce)
            .ToListAsync();
        }

    }
}