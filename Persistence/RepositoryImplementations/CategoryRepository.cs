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
    public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(Context context)
        {
            _context = context;
        }

        public async Task<Category> GetAsync(Guid id)
        {
            return await _context.Categories
            .SingleOrDefaultAsync(a => a.Id == id && !a.IsDeleted);
        }

        public async Task<Category> GetAsync(Expression<Func<Category, bool>> expression)
        {
            return await _context.Categories
            .Where(a => !a.IsDeleted )
            .SingleOrDefaultAsync(expression);
        }

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            return await _context.Categories.AsNoTracking()
           .Where(a => !a.IsDeleted )
           .ToListAsync();
        }

        public async Task<IEnumerable<Category>> GetSelectedAsync(List<Guid> ids)
        {
            return await _context.Categories
            .Where(a => ids.Contains(a.Id) && !a.IsDeleted )
            .ToListAsync();
        }

        public async Task<IEnumerable<Category>> GetSelectedAsync(Expression<Func<Category, bool>> expression)
        {
            return await _context.Categories
            .Where(expression)
            .ToListAsync();
        }

       
    }
}