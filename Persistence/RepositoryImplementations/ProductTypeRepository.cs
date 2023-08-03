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
    public class ProductTypeRepository : BaseRepository<ProductType>, IProductTypeRepository
    {
        public ProductTypeRepository(Context context)
        {
            _context = context;
        }

        public async Task<ProductType> GetAsync(Guid id)
        {
            return await _context.ProductTypes
            .Where(proType => !proType.IsDeleted)
            .Include(op => op.OrderProductTypes)
            .ThenInclude(o => o.Order)
            .SingleOrDefaultAsync(prod => prod.Id == id);
        }

       
        public async Task<ProductType> GetAsync(Expression<Func<ProductType, bool>> expression)
        {
            return await _context.ProductTypes
            .Where(prod => !prod.IsDeleted)
            .Include(op => op.OrderProductTypes)
            .ThenInclude(o => o.Order)
            .SingleOrDefaultAsync(expression);
        }

        public async Task<IEnumerable<ProductType>> GetAllAsync()
        {
            return await _context.ProductTypes.AsNoTracking()
            .Where(prod => !prod.IsDeleted)
            // .Include(a => a.Produce)
            .Include(a => a.OrderProductTypes)
            .ToListAsync();
        }

      
        public async Task<IEnumerable<ProductType>> GetSelectedAsync(Expression<Func<ProductType, bool>> expression)
        {
            return await _context.ProductTypes
            // .Include(p => p.Produce)
            .Include(a => a.OrderProductTypes)
            .ThenInclude(o => o.Order)
            .Where(expression)
            .ToListAsync();
        }

        public async Task<IEnumerable<ProductType>> GetSelectedAsync(List<Guid> ids)
        {
            return await _context.ProductTypes
            // .Include(p => p.Produce)
            .Include(a => a.OrderProductTypes)
            .ThenInclude(o => o.Order)
            .Where(a => ids.Contains(a.Id) && !a.IsDeleted)
            .ToListAsync();
        }

    }
}