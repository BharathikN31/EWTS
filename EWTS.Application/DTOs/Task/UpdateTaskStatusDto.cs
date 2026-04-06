using EWTS.Domain.Enums;

namespace EWTS.Application.DTOs.Task
{
    public class UpdateTaskStatusDto
    {
        public Guid TaskId { get; set; }

        public TaskItemStatus Status { get; set; }
    }
}
