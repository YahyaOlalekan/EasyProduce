using System.Threading.Tasks;
using Application.Abstractions.RepositoryInterfaces;
using Domain.Entity;
using Persistence.AppDbContext;

namespace Persistence.RepositoryImplementations
{
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity, new()
    {
        protected Context _context;

        public async Task<T> CreateAsync(T entity)
        {
           await _context.Set<T>().AddAsync(entity);
            return entity;
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public T Update(T entity)
        {
           _context.Set<T>().Update(entity);
            return entity;
        }
        public T Delete(T entity)
        {
             _context.Set<T>().Remove(entity);
            return entity;
        }
    }
}