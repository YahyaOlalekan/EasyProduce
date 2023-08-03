using System.Threading.Tasks;

namespace Application.Abstractions.RepositoryInterfaces
{
    public interface IBaseRepository<T>
    {
        Task<T> CreateAsync(T entity);
        T Delete(T entity);
        T Update(T entity);
        Task SaveAsync();
    }
}