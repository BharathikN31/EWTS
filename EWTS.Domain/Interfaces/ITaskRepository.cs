using EWTS.Domain.Entities;

namespace EWTS.Domain.Interfaces
{
    public interface ITaskRepository
    {
        Task<TaskItem> CreateAsync(TaskItem task);
        Task<List<TaskItem>> GetAllAsync();
        Task<TaskItem?> GetByIdAsync(Guid id);
        Task UpdateAsync(TaskItem task);

        Task AddApprovalAsync(Approval approval);
    }
}