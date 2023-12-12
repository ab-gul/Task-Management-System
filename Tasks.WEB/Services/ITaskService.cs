using Tasks.WEB.Models;

namespace Tasks.WEB.Services
{
    public interface ITaskService
    {
        Task<IEnumerable<TaskDTO>> GetAllAsync();
        Task<TaskDTO> GetByIdAsync(Guid id);
        Task<TaskDTO> AddAsync(TaskDTO entity);
        Task UpdateAsync(TaskDTO entity);
        Task DeleteAsync(Guid id);
    }
}