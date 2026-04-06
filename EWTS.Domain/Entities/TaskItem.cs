using EWTS.Domain.Enums;
namespace EWTS.Domain.Entities
{
    public class TaskItem
    {
        public Guid Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public TaskItemStatus Status { get; set; } = TaskItemStatus.Pending;

        public Guid AssignedToUserId { get; set; }

        public User AssignedToUser { get; set; } = null!;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}