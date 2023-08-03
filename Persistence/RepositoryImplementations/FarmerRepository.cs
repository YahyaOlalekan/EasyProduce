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
    public class FarmerRepository : BaseRepository<Farmer>, IFarmerRepository
    {
        public FarmerRepository(Context context)
        {
            _context = context;
        }

        public async Task<Farmer> GetAsync(Guid id)
        {
            return await _context.Farmers
            .Include(a => a.Transactions)
             .Include(a => a.User)
             .SingleOrDefaultAsync(a => a.Id == id && !a.IsDeleted );
        }

        public async Task<Farmer> GetAsync(Expression<Func<Farmer, bool>> expression)
        {
            return await _context.Farmers
            .Where((Farmer a) => !a.IsDeleted)
            .Include(a => a.Transactions)
             .Include(a => a.User)
            .SingleOrDefaultAsync(expression);
        }

        public async Task<IEnumerable<Farmer>> GetAllAsync(Expression<Func<Farmer, bool>> expression)
        {
            return await _context.Farmers 
            .Include(a => a.User)
            .Include(a => a.Transactions)
            .Where(expression)
            .ToListAsync();
        }



        public async Task<IEnumerable<Farmer>>  GetAllAsync()
        {
            return await _context.Farmers.AsNoTracking()
            .Where(a => !a.IsDeleted)
            .Include(a => a.User)
            .Include(a => a.Transactions)
            .ToListAsync();
        }

       
         public async Task<IEnumerable<Farmer>> GetSelectedAsync(List<Guid> ids)
        {
            return await _context.Farmers
            .Where(a => ids.Contains(a.Id) && !a.IsDeleted)
            .Include(a => a.User)
            .Include(a => a.Transactions)
            .ToListAsync();
        }


        public async Task<IEnumerable<Farmer>> GetSelectedAsync(Expression<Func<Farmer, bool>> expression)
        {
            return await _context.Farmers
            .Where(expression)
            .Include(a => a.Transactions)
            .Include(a => a.User)
            .ToListAsync();
        }

       
    }
}