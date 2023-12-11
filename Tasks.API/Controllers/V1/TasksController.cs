﻿using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using Tasks.API.Contracts.V1;
using Tasks.API.Domain;
using Tasks.API.Services;
using static Tasks.API.Contracts.V1.Tasks;

namespace Tasks.API.Controllers.V1
{

    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly ITaskService _taskService;
        public TaskController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        [HttpGet(ApiRoutes.Tasks.GetAll)]
        public async Task<IActionResult> GetAllTasksAsync()
        {
            var tasks = await _taskService.GetAllTasksAync();
            return Ok(tasks);
        }

        [HttpGet(ApiRoutes.Tasks.Get)]
        public async Task<IActionResult> GetTaskByIdAsync([FromRoute] Guid id)
        {
            var Task = await _taskService.GetTaskByIdAsync(id);

            return Task != null
                ? Ok((GetTaskResponse)Task)
                : NotFound($"Task with given id: {id} does not exist!");
        }


        [HttpDelete(ApiRoutes.Tasks.Delete)]
        public async Task<IActionResult> DeleteTaskAsync([FromRoute] Guid id)
        {
            var deleted = await _taskService.DeleteTaskAsync(id);

            if (deleted == false) return NotFound($"Task with given id: {id} does not exist!");

            return NoContent();
        }

        [HttpPut(ApiRoutes.Tasks.Update)]
        public async Task<IActionResult> UpdateTaskAsync([FromRoute] Guid id, [FromBody] UpdateTaskRequest request)
        {
            var taskToBeUpdated = await _taskService.GetTaskByIdAsync(id);

            if (taskToBeUpdated == null) return NotFound($"Task with given Id: {id} does not exist!");

            taskToBeUpdated.Edit(
                request.title,
                request.description,
                request.status != null
                ? (Status)Enum.Parse(typeof(Status), request.status, true)
                : null);

            await _taskService.UpdateTaskAsync(taskToBeUpdated);

            return NoContent();
        }


        [HttpPost(ApiRoutes.Tasks.Create)]
        public async Task<IActionResult> CreateTaskAsync([FromBody] CreateTaskRequest request)
        {
            var newTask = (TaskItem)request;

            var addedTask = await _taskService.AddTaskAsync(newTask);

            return Ok((CreateTaskResponse)addedTask);
        }

    }
}
