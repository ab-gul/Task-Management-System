using System.Net.Http.Json;
using Tasks.WEB.Models;

namespace Tasks.WEB.Services
{
    public class TaskService : ITaskService
    {
        private readonly HttpClient _httpClient;

        private readonly static string endpoint = "api/v1/tasks";
        public TaskService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task DeleteAsync(Guid id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"{endpoint}/{id}");
                response.EnsureSuccessStatusCode();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<TaskDTO> GetByIdAsync(Guid id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{endpoint}/{id}");
                response.EnsureSuccessStatusCode();
                return ((await response.Content.ReadFromJsonAsync<TaskDTO>())!);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<TaskListDTO> GetAllAsync(int pageNumber = 1, int pageSize = 10)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{endpoint}?pageNumber={pageNumber}&pageSize={pageSize}");
                response.EnsureSuccessStatusCode();
                return ((await response.Content.ReadFromJsonAsync<TaskListDTO>()) ?? new TaskListDTO());
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<TaskDTO> AddAsync(TaskDTO entity)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync(
                    endpoint, 
                    new { 
                        entity.Title, 
                        entity.Description,
                        entity.DueDate 
                    });
                response.EnsureSuccessStatusCode();
                return ((await response.Content.ReadFromJsonAsync<TaskDTO>())!);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task UpdateAsync(TaskDTO entity)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"{endpoint}/{entity.Id}", 
                    new { entity.Title , entity.Description, entity.Status });
                response.EnsureSuccessStatusCode();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}