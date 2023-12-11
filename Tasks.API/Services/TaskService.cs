using Tasks.API.Data.Abstract;
using Tasks.API.Domain;

namespace Tasks.API.Services
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _taskRepository;

        public TaskService(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;

        }

        public async Task<TaskItem> AddTaskAsync(TaskItem newTask)
        {
            return await _taskRepository.CreateAsync(newTask);
        }

        public async Task<bool> DeleteTaskAsync(Guid id)
        {
            return await _taskRepository.DeleteAsync(id);

        }

        public async Task<List<TaskItem>> GetAllTasksAync()
        {
            return await _taskRepository.GetAllAsync();
        }

        public async Task<TaskItem?> GetTaskByIdAsync(Guid id)
        {
            return await _taskRepository.GetByIdAsync(id);
        }

        public async Task UpdateTaskAsync(TaskItem newTask)
        {
            await _taskRepository.UpdateAsync(newTask);
        }
    }
}
