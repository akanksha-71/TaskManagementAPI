using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TaskManagementAPI.Models;
using TaskManagementAPI.Services;
using System;
using System.Threading.Tasks;

namespace TaskManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly ITaskService _taskService;
        private readonly ILogger<TasksController> _logger;

        public TasksController(ITaskService taskService, ILogger<TasksController> logger)
        {
            _taskService = taskService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetTasks([FromQuery] string status, [FromQuery] DateTime? dueDate, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            try
            {
                var tasks = await _taskService.GetTasks(status, dueDate, pageNumber, pageSize);
                return Ok(tasks);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching tasks.");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTask(int id)
        {
            var task = await _taskService.GetTaskById(id);
            if (task == null)
                return NotFound("Task not found.");
            return Ok(task);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTask([FromBody] TodoTask task)
        {
            if (task == null || string.IsNullOrEmpty(task.Title) || task.DueDate == default)
                return BadRequest("Invalid task data.");

            var createdTask = await _taskService.CreateTask(task);
            return CreatedAtAction(nameof(GetTask), new { id = createdTask.Id }, createdTask);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTask(int id, [FromBody] TodoTask task)
        {
            var updatedTask = await _taskService.UpdateTask(id, task);
            if (updatedTask == null)
                return NotFound("Task not found.");
            return Ok(updatedTask);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            var success = await _taskService.DeleteTask(id);
            if (!success)
                return NotFound("Task not found.");
            return NoContent();
        }
    }
}
