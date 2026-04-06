using EWTS.Domain.Enums;

namespace EWTS.Application.DTOs.Task
{
    public class ApproveTaskDto
    {
        public Guid TaskId { get; set; }

        public ApprovalStatus Status { get; set; }

        public string? Comments { get; set; }
    }
}