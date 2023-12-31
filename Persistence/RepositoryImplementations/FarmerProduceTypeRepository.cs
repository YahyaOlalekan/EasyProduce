using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Application.Abstractions.RepositoryInterfaces;
using Application.Dtos;
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
            .ThenInclude(x => x.User)
            .Include(a => a.ProduceType)
            .ThenInclude(a => a.Produce)
            .ThenInclude(a => a.Category)
            .Where(a => a.FarmerId == id && !a.IsDeleted).ToListAsync();
        }
      

        public async Task<IEnumerable<FarmerProduceType>> GetAllAsync(Expression<Func<FarmerProduceType, bool>> expression)
        {
            return await _context.FarmerProduceTypes
            .Where(expression)
            .Include(a => a.Farmer)
            .ThenInclude(a => a.User)
            .Include(a => a.ProduceType)
            // .ThenInclude(a => a.Produce)
            // .ThenInclude(a => a.Category)
            .ToListAsync();
        }
        public async Task<IEnumerable<ProduceType>> GetAllApprovedProduceTypeOfAFarmer(Guid farmerId)
        {
            return await _context.FarmerProduceTypes
            .Where(a => a.FarmerId == farmerId && a.Status == Domain.Enum.Status.Approved && !a.IsDeleted)
             .Select(a => a.ProduceType)
             .ToListAsync();
        }
        public async Task<FarmerProduceType> GetAsync(Expression<Func<FarmerProduceType, bool>> expression)
        {
            return await _context.FarmerProduceTypes
            .Where(expression)
            .Include(a => a.Farmer)
            .ThenInclude(a=> a.User)
            .Include(a => a.ProduceType)
            .FirstOrDefaultAsync();
        }
        public async Task<FarmerProduceType> GetAsync(Guid farmerId, Guid produceTypeId)
        {
            return await _context.FarmerProduceTypes
            .Include(a => a.Farmer)
            .Include(a => a.ProduceType)
            .Where(x => x.FarmerId == farmerId && x.ProduceTypeId == produceTypeId && !x.IsDeleted)
            .FirstOrDefaultAsync();
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
            .ThenInclude(a=> a.User)
            .Include(a => a.ProduceType)
            .ThenInclude(a=> a.Produce)
            .ThenInclude(a=> a.Category)
            .ToListAsync();
        }

   
}
