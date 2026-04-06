using EWTS.Application.DTOs.Task;

namespace EWTS.Application.Interfaces
{
    public interface ITaskService
    {
        Task<TaskDto> CreateAsync(CreateTaskDto dto);
        Task<List<TaskDto>> GetAllAsync();
        Task<TaskDto?> GetByIdAsync(Guid id);
        Task<TaskDto?> UpdateStatusAsync(UpdateTaskStatusDto dto, Guid userId);
        Task<List<TaskDto>> GetMyTasksAsync(Guid userId);
        Task<List<TaskDto>> GetFilteredTasksAsync(TaskFilterDto filter, string role, Guid userId);
        Task<string> ApproveTaskAsync(ApproveTaskDto dto, Guid userId, string role);
    }
}