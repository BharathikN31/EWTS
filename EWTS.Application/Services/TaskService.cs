using EWTS.Application.DTOs.Task;
using EWTS.Application.Interfaces;
using EWTS.Domain.Entities;
using EWTS.Domain.Interfaces;

namespace EWTS.Application.Services
{
    public class TaskService : ITaskService
    {
        private readonly IActivityService _activityService;
        private readonly ITaskRepository _taskRepository;

        public TaskService(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public async Task<TaskDto> CreateAsync(CreateTaskDto dto)
        {
            var task = new TaskItem
            {
                Id = Guid.NewGuid(),
                Title = dto.Title,
                Description = dto.Description,
                AssignedToUserId = dto.AssignedToUserId
            };

            var created = await _taskRepository.CreateAsync(task);

await _activityService.LogAsync(
    created.AssignedToUserId,
    "Task Created",
    $"Task '{created.Title}' assigned"
);

            return new TaskDto
            {
                Id = created.Id,
                Title = created.Title,
                Description = created.Description,
                Status = created.Status,
                AssignedToUserId = created.AssignedToUserId
            };
           

        }

        public async Task<List<TaskDto>> GetAllAsync()
        {
            var tasks = await _taskRepository.GetAllAsync();

            return tasks.Select(t => new TaskDto
            {
                Id = t.Id,
                Title = t.Title,
                Description = t.Description,
                Status = t.Status,
                AssignedToUserId = t.AssignedToUserId
            }).ToList();
        }

        public async Task<TaskDto?> GetByIdAsync(Guid id)
        {
            var task = await _taskRepository.GetByIdAsync(id);

            if (task == null) return null;

            return new TaskDto
            {
                Id = task.Id,
                Title = task.Title,
                Description = task.Description,
                Status = task.Status,
                AssignedToUserId = task.AssignedToUserId
            };
        }
        public async Task<TaskDto?> UpdateStatusAsync(UpdateTaskStatusDto dto, Guid userId)
        {
            var task = await _taskRepository.GetByIdAsync(dto.TaskId);

            if (task == null)
                throw new Exception("Task not found");

            //  ONLY ASSIGNED USER CAN UPDATE
            if (task.AssignedToUserId != userId)
                throw new Exception("You are not allowed to update this task");

            task.Status = dto.Status;

await _taskRepository.UpdateAsync(task);

await _activityService.LogAsync(
    userId,
    "Task Updated",
    $"Task status changed to {task.Status}"
);
            await _taskRepository.UpdateAsync(task);

            return new TaskDto
            {
                Id = task.Id,
                Title = task.Title,
                Description = task.Description,
                Status = task.Status,
                AssignedToUserId = task.AssignedToUserId
            };

            
        }

        public async Task<List<TaskDto>> GetMyTasksAsync(Guid userId)
        {
            var tasks = await _taskRepository.GetAllAsync();

            return tasks
                .Where(t => t.AssignedToUserId == userId)
                .Select(t => new TaskDto
                {
                    Id = t.Id,
                    Title = t.Title,
                    Description = t.Description,
                    Status = t.Status,
                    AssignedToUserId = t.AssignedToUserId
                })
                .ToList();
        }

        public async Task<List<TaskDto>> GetFilteredTasksAsync(TaskFilterDto filter, string role, Guid userId)
        {
            var tasks = await _taskRepository.GetAllAsync();

            // 👤 Employee → only own tasks
            if (role == "Employee")
            {
                tasks = tasks.Where(t => t.AssignedToUserId == userId).ToList();
            }

            // 🔍 Filter by status
            if (filter.Status.HasValue)
            {
                tasks = tasks.Where(t => t.Status == filter.Status.Value).ToList();
            }

            return tasks.Select(t => new TaskDto
            {
                Id = t.Id,
                Title = t.Title,
                Description = t.Description,
                Status = t.Status,
                AssignedToUserId = t.AssignedToUserId
            }).ToList();
        }
        public async Task<string> ApproveTaskAsync(ApproveTaskDto dto, Guid userId, string role)
        {
            if (role != "Manager" && role != "Admin")
                throw new Exception("Only Manager/Admin can approve tasks");

            var task = await _taskRepository.GetByIdAsync(dto.TaskId);

            if (task == null)
                throw new Exception("Task not found");

            // ❗ Only completed tasks can be approved
            if (task.Status != Domain.Enums.TaskItemStatus.Completed)
                throw new Exception("Task must be completed before approval");

            var approval = new Approval
            {
                Id = Guid.NewGuid(),
                TaskId = dto.TaskId,
                ApprovedByUserId = userId,
                Status = dto.Status,
                Comments = dto.Comments
            };

            await _taskRepository.AddApprovalAsync(approval);

            // 🔥 Update Task based on approval
            if (dto.Status == Domain.Enums.ApprovalStatus.Approved)
            {
                task.Status = Domain.Enums.TaskItemStatus.Completed;
            }
            else
            {
                task.Status = Domain.Enums.TaskItemStatus.InProgress;
            }

            await _taskRepository.UpdateAsync(task);

            await _activityService.LogAsync(
    userId,
    "Task Approved",
    $"Task {dto.TaskId} approved"
);

            return "Approval processed successfully";

           
        }

        public TaskService(
    ITaskRepository taskRepository,
    IActivityService activityService)
{
    _taskRepository = taskRepository;
    _activityService = activityService;
}
    }
}