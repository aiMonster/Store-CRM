using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StoreCRM.DTOs;
using StoreCRM.Enums;
using StoreCRM.Interfaces;
using StoreCRM.Services;

namespace StoreCRM.Controllers
{
    [Authorize]
    [Route("tasks/")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly ITasksService _tasksService;

        public TasksController(ITasksService tasksService)
		{
            _tasksService = tasksService;
		}

        // <summary>
        /// Get all tasks
        /// </summary>
        /// <returns></returns>
        /// <response code="200">An array of tasks</response>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaskDTO>>> GetAll()
        {
            return Ok(await _tasksService.GetAllAsync());
        }

        /// <summary>
        /// Add new task
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Added task id</response>
        /// <response code="400">Bad input parameter(s)</response>
        [HttpPost]
        public async Task<ActionResult> AddNewTask([FromBody] CreateTaskDTO task)
        {
            try
            {
                return Ok(await _tasksService.CreateTaskAsync(task));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Updates task status
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Success</response>
        /// <response code="400">Bad input parameter(s)</response>
        [HttpPut("{taskId}/status")]
        public async Task<ActionResult> UpdateTaskStatus([FromRoute] int taskId, [FromQuery] StoreTaskStatus status)
        {
            try
            {
                await _tasksService.UpdateStatusAsync(taskId, status);

                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Updates task asignee
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Success</response>
        /// <response code="400">Bad input parameter(s)</response>
        [HttpPut("{taskId}/assignee")]
        public async Task<ActionResult> UpdateTaskAsignee([FromRoute] int taskId, [FromQuery] Guid? assigneeId)
        {
            try
            {
                await _tasksService.UpdateAssigneeAsync(taskId, assigneeId);

                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
