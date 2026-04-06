using EWTS.Domain.Enums;

namespace EWTS.Application.DTOs.Task
{
    public class TaskDto
    {
        public Guid Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public TaskItemStatus Status { get; set; }

        public Guid AssignedToUserId { get; set; }
    }
}