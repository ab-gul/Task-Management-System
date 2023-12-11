using Microsoft.OpenApi.Extensions;
using Tasks.API.Domain;

namespace Tasks.API.Contracts.V1
{
    public class Tasks
    {
        #region Requests
        public record CreateTaskRequest(string title, string description, DateTime dueDate)
        {
            public static explicit operator TaskItem(CreateTaskRequest request)
            {
                return new TaskItem(
                    request.title,
                    request.description,
                    request.dueDate);
            }

        }

        public record GetAllTasksRequest(int? pageNumber, int? pageSize);
        public record UpdateTaskRequest(string? title, string? description, string? status);
        #endregion

        #region Responses
        public record GetTaskResponse(Guid id, string title, string description, DateTime dueDate, Status status)
        {
            public static explicit operator GetTaskResponse(TaskItem task)
            {
                return new GetTaskResponse(task.Id, task.Title, task.Description, task.DueDate, task.Status);
            }
        }
        public record CreateTaskResponse(Guid id ,string title, string description, DateTime dueDate, Status status)
        {
            public static explicit operator CreateTaskResponse(TaskItem task)
            {
                return new CreateTaskResponse(task.Id, task.Title, task.Description, task.DueDate, task.Status);
            }
        }
        #endregion
    }
}
