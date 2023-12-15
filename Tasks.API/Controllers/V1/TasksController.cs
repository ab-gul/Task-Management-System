using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using Tasks.API.Contracts.V1;
using Tasks.API.Data.Abstract;
using Tasks.API.Domain;
using Tasks.API.Pagination;
using Tasks.API.Validation;
using static Tasks.API.Contracts.V1.Tasks;

namespace Tasks.API.Controllers.V1
{

    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly ITaskRepository _taskRepo;
        public TaskController(ITaskRepository taskRepo)
        {
            _taskRepo = taskRepo;
        }

        [HttpGet(ApiRoutes.Tasks.GetAll)]
        public async Task<IActionResult> GetAllTasksAsync(
            [FromQuery] GetAllTasksRequest request,
            [FromServices] IValidator<GetAllTasksRequest>? validator)
        {
            ArgumentException.ThrowIfNullOrEmpty(nameof(validator));

            ValidationResult validationResult = validator!.Validate(request);

            if (!validationResult.IsValid)
            {
                validationResult.AddToModelState(this.ModelState);

                return ValidationProblem();
            }

            var response = await _taskRepo.GetPaginatedAsync(
                new PaginationFilter(request?.pageNumber, request?.pageSize),
                t => new GetTaskResponse(t.Id, t.Title, t.Description, t.DueDate, t.Status));

            return Ok(response);
        }

        [HttpGet(ApiRoutes.Tasks.Get)]
        public async Task<IActionResult> GetTaskByIdAsync([FromRoute] Guid id)
        {
            var Task = await _taskRepo.GetByIdAsync(id);

            return Task != null
                ? Ok((GetTaskResponse)Task)
                : NotFound($"Task with given id: {id} does not exist!");
        }


        [HttpDelete(ApiRoutes.Tasks.Delete)]
        public async Task<IActionResult> DeleteTaskAsync([FromRoute] Guid id)
        {
            var deleted = await _taskRepo.DeleteAsync(id);

            if (deleted == false) return NotFound($"Task with given id: {id} does not exist!");

            return NoContent();
        }

        [HttpPut(ApiRoutes.Tasks.Update)]
        public async Task<IActionResult> UpdateTaskAsync(
            [FromRoute] Guid id,
            [FromBody] JsonElement request,
            [FromServices] IValidator<JsonElement>? validator)
        {
            ArgumentException.ThrowIfNullOrEmpty(nameof(validator));

            ValidationResult validationResult = validator!.Validate(request);

            if (!validationResult.IsValid)
            {
                validationResult.AddToModelState(this.ModelState);

                return ValidationProblem();
            }

            var taskToBeUpdated = await _taskRepo.GetByIdAsync(id);

            if (taskToBeUpdated == null) { return NotFound($"Task with given Id: {id} does not exist!"); }

            UpdateTaskRequest? updateRequestModel = JsonSerializer.Deserialize<UpdateTaskRequest>(request.GetRawText()); 
        

                    taskToBeUpdated.Edit(
                        updateRequestModel!.title,
                        updateRequestModel.description,
                        updateRequestModel.status != null
                        ? (Status)Enum.Parse(typeof(Status), updateRequestModel.status, true)
                        : null);

            await _taskRepo.UpdateAsync(taskToBeUpdated);

            return NoContent();
        }

        [HttpPost(ApiRoutes.Tasks.Create)]
        public async Task<IActionResult> CreateTaskAsync(
            [FromBody] CreateTaskRequest request,
            [FromServices] IValidator<CreateTaskRequest>? validator)
        {
            ArgumentException.ThrowIfNullOrEmpty(nameof(validator));

            ValidationResult validationResult = validator!.Validate(request);

            if (!validationResult.IsValid)
            {
                validationResult.AddToModelState(this.ModelState);

                return ValidationProblem();
            }

            var addedTask = await _taskRepo.CreateAsync((TaskItem)request);

            return Ok((CreateTaskResponse)addedTask);
        }

    }
}

