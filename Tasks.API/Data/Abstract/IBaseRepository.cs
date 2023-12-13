using System.Linq.Expressions;
using Tasks.API.Domain;
using Tasks.API.Pagination;

namespace Tasks.API.Data.Abstract
{
    public interface IBaseRepository<T> where T : Base
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T?> GetByIdAsync(Guid id);
        Task<T> CreateAsync(T entity);
        Task UpdateAsync(T entity);
        Task<bool> DeleteAsync(Guid id);
    }
}
