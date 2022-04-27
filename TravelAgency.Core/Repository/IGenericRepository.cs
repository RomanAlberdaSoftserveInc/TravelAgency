using System.Collections.Generic;
using System.Threading.Tasks;

namespace TravelAgency.Core.Repository
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> GetByIdAsync(int id);
        Task<IReadOnlyList<T>> GetAllAsync();
        Task<int> AddAsync(T entity);
        Task<int> UpdateAsync(T entity);
        Task<bool> DeleteAsync(T entity);
    }
}
