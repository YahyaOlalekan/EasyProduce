using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Application.Abstractions.RepositoryInterfaces;
using Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Persistence.AppDbContext;

namespace Persistence.RepositoryImplementations;
public class FarmerProduceTypeRepository : BaseRepository<FarmerProduceType>, IFarmerProduceTypeRepository
{
     public FarmerProduceTypeRepository(Context context)
        {
            _context = context;
        }

        public async Task<bool> CreateFarmerProduceTypeAsync(List<FarmerProduceType> farmers)
        {
            await  _context.AddRangeAsync(farmers);
            return true;
        }

        public async Task<List<FarmerProduceType>> GetAsync(Guid id)
        {
            return await _context.FarmerProduceTypes
            .Include(a => a.Farmer)
            .Include(a => a.ProduceType)
            .ThenInclude(a => a.Produce)
            .ThenInclude(a => a.Category)
            .Where(a => a.FarmerId == id && !a.IsDeleted).ToListAsync();
        }

        public async Task<FarmerProduceType> GetAsync(Expression<Func<FarmerProduceType, bool>> expression)
        {
            return await _context.FarmerProduceTypes
            .Where(a => !a.IsDeleted)
            .Include(a => a.Farmer)
            .Include(a => a.ProduceType)
            .SingleOrDefaultAsync(expression);
        }

        public async Task<IEnumerable<FarmerProduceType>> GetAllAsync()
        {
            return await _context.FarmerProduceTypes.AsNoTracking()
           .Where(a => !a.IsDeleted)
           .ToListAsync();
        }

        public async Task<IEnumerable<FarmerProduceType>> GetSelectedAsync(List<Guid> ids)
        {
            return await _context.FarmerProduceTypes
            .Where(a => ids.Contains(a.Id) && !a.IsDeleted)
            .Include(a => a.Farmer)
            .Include(a => a.ProduceType)
            .ToListAsync();
        }

        public async Task<IEnumerable<FarmerProduceType>> GetSelectedAsync(Expression<Func<FarmerProduceType, bool>> expression)
        {
            return await _context.FarmerProduceTypes
            .Where(expression)
            .Include(a => a.Farmer)
            .Include(a => a.ProduceType)
            .ToListAsync();
        }

    // Task<FarmerProduceType> IFarmerProduceTypeRepository.GetAsync(Guid id)
    // {
    //     throw new NotImplementedException();
    // }
}
