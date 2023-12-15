using Tasks.WEB.Models;

namespace Tasks.WEB.Services
{
    public interface ITaskService
    {
        Task<TaskListDTO> GetAllAsync(int pageNumber = 1, int pageSize = 10);
        Task<TaskDTO> GetByIdAsync(Guid id);
        Task<TaskDTO> AddAsync(TaskDTO entity);
        Task UpdateAsync(TaskDTO entity);
        Task DeleteAsync(Guid id);
    }
}