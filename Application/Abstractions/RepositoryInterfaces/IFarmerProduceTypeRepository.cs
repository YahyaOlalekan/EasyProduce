using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Domain.Entity;


namespace Application.Abstractions.RepositoryInterfaces;
public interface IFarmerProduceTypeRepository : IBaseRepository<FarmerProduceType>
{
    Task<bool> CreateFarmerProduceTypeAsync(List<FarmerProduceType> Farmers);
    Task<List<FarmerProduceType>> GetAsync(Guid id);
    // Task<FarmerProduceType> GetAsync(Guid id);
    Task<FarmerProduceType> GetAsync(Expression<Func<FarmerProduceType, bool>> expression);
    Task<IEnumerable<FarmerProduceType>> GetSelectedAsync(List<Guid> ids);
    Task<IEnumerable<FarmerProduceType>> GetSelectedAsync(Expression<Func<FarmerProduceType, bool>> expression);
    Task<IEnumerable<FarmerProduceType>> GetAllAsync();
    // Task<IEnumerable<FarmerProduceType>> GetAllAsync(Expression<Func<FarmerProduceType, bool>> expression);
     Task<IEnumerable<ProduceType>> GetAllApprovedProduceTypeOfAFarmer(Guid farmerId);

}
