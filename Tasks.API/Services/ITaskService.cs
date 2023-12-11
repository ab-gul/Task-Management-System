using Tasks.API.Domain;

namespace Tasks.API.Services
{
    public interface ITaskService
    {
        Task<List<TaskItem>> GetAllTasksAync();
        Task<TaskItem?> GetTaskByIdAsync(Guid id);
        Task<bool> DeleteTaskAsync(Guid id);
        Task<TaskItem> AddTaskAsync(TaskItem newTask);
        Task UpdateTaskAsync(TaskItem newTask);
    }
}
