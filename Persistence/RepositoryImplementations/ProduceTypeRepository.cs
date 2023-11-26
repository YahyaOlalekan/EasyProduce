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
    public class ProduceTypeRepository : BaseRepository<ProduceType>, IProduceTypeRepository
    {
        public ProduceTypeRepository(Context context)
        {
            _context = context;
        }

        public async Task<ProduceType> GetAsync(Guid id)
        {
            return await _context.ProduceTypes
            .Where(a => !a.IsDeleted)
            // .Include(a => a.TransactionProduceTypes)
            // .Include(a => a.Produce)
            // .ThenInclude(a => a.Category)
            .SingleOrDefaultAsync(x => x.Id == id);
        }

       
        public async Task<ProduceType> GetAsync(Expression<Func<ProduceType, bool>> expression)
        {
            return await _context.ProduceTypes
            .Where(a => !a.IsDeleted)
            // .Include(a => a.TransactionProduceTypes)
            .SingleOrDefaultAsync(expression);
        }
        // public async Task<ProduceType> GetMatchigAsync(Expression<Func<ProduceType, bool>> expression)
        // {
        //     return await _context.ProduceTypes
        //     .Where(a => List<ProduceTypeId>)
        //     .Include(a => a.TransactionProduceTypes)
        //     .SingleOrDefaultAsync(expression);
        // }

       
        public async Task<IEnumerable<ProduceType>> GetAllAsync()
        {
            // return await _context.ProduceTypes.AsNoTracking()
            return await _context.ProduceTypes
            .Where(a => !a.IsDeleted)
            .Include(a => a.Produce)
            .ThenInclude(a => a.Category)
            .Include(a => a.FarmerProduceTypes)
            .ToListAsync();
        }

      
        public async Task<IEnumerable<ProduceType>> GetSelectedAsync(Expression<Func<ProduceType, bool>> expression)
        {
            return await _context.ProduceTypes
            // .Include(a => a.TransactionProduceTypes)
            .Where(expression)
            .ToListAsync();
        }

        public async Task<IEnumerable<ProduceType>> GetSelectedAsync(List<Guid> ids)
        {
            return await _context.ProduceTypes
            // .Include(a => a.TransactionProduceTypes)
            .Where(a => ids.Contains(a.Id) && !a.IsDeleted)
            .ToListAsync();
        }


    }
}