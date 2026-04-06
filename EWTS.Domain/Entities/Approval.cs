using EWTS.Domain.Enums;

namespace EWTS.Domain.Entities
{
    public class Approval
    {
        public Guid Id { get; set; }

        public Guid TaskId { get; set; }

        public TaskItem Task { get; set; } = null!;

        public Guid ApprovedByUserId { get; set; }

        public User ApprovedByUser { get; set; } = null!;

        public ApprovalStatus Status { get; set; }

        public string? Comments { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}