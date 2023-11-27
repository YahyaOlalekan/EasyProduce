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
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        public ProductRepository(Context context)
        {
            _context = context;
        }

        // public async Task<Product> GetAsync(Guid id)
        // {
        //     return await _context.Products
        //     .Where(prod => !prod.IsDeleted)
        //     .Include(p => p.Group)
        //     .SingleOrDefaultAsync(prod => prod.Id == id);
        // }

       
        // public async Task<Product> GetAsync(Expression<Func<Product, bool>> expression)
        // {
        //     return await _context.Products
        //     .Where(prod => !prod.IsDeleted)
        //     .Include(p => p.Category)
        //     .SingleOrDefaultAsync(expression);
        // }

       
        // public async Task<IEnumerable<Product>> GetAllAsync()
        // {
        //     return await _context.Products.AsNoTracking()
        //     .Where(prod => !prod.IsDeleted)
        //     .Include(a => a.Category)
        //     .ToListAsync();
        // }

      
        // public async Task<IEnumerable<Product>> GetSelectedAsync(Expression<Func<Product, bool>> expression)
        // {
        //     return await _context.Products
        //     .Include(p => p.Category)
        //     .Where(expression)
        //     .ToListAsync();
        // }

        // public async Task<IEnumerable<Product>> GetSelectedAsync(List<Guid> ids)
        // {
        //     return await _context.Products
        //     .Include(p => p.Category)
        //     .Where(a => ids.Contains(a.Id) && !a.IsDeleted)
        //     .ToListAsync();
        // }

       
    }
}