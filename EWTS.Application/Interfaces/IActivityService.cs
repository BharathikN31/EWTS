using EWTS.Domain.Entities;

namespace EWTS.Application.Interfaces
{
    public interface IActivityService
    {
        Task LogAsync(Guid userId, string action, string description);
        Task<List<ActivityLog>> GetRecentAsync(int count = 50);   // 🔹 NEW
    }
}