using EWTS.Application.DTOs.Task;
using EWTS.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EWTS.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class TaskController : ControllerBase
    {
        private readonly ITaskService _taskService;

        public TaskController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateTaskDto dto)
        {
            var result = await _taskService.CreateAsync(dto);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _taskService.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _taskService.GetByIdAsync(id);
            return Ok(result);
        }


        [HttpPut("status")]
        public async Task<IActionResult> UpdateStatus(UpdateTaskStatusDto dto)
        {
            var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

            var result = await _taskService.UpdateStatusAsync(dto, userId);

            return Ok(result);
        }

        [HttpGet("my-tasks")]
        public async Task<IActionResult> GetMyTasks()
        {
            var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

            var tasks = await _taskService.GetMyTasksAsync(userId);

            return Ok(tasks);
        }

        [HttpPost("filter")]
        public async Task<IActionResult> FilterTasks(TaskFilterDto filter)
        {
            var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            var role = User.FindFirst(ClaimTypes.Role)?.Value!;

            var tasks = await _taskService.GetFilteredTasksAsync(filter, role, userId);

            return Ok(tasks);
        }

        [HttpPost("approve")]
        public async Task<IActionResult> ApproveTask(ApproveTaskDto dto)
        {
            var userId = Guid.Parse(User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)!.Value);
            var role = User.FindFirst(System.Security.Claims.ClaimTypes.Role)?.Value!;

            var result = await _taskService.ApproveTaskAsync(dto, userId, role);

            return Ok(result);
        }
    }
}