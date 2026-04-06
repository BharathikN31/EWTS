using EWTS.Domain.Enums;

namespace EWTS.Application.DTOs.Task
{
    public class TaskFilterDto
    {
        public TaskItemStatus? Status { get; set; }
    }
}