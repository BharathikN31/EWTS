namespace EWTS.Domain.Entities
{
    public class ActivityLog
    {
        public Guid Id { get; set; }

        public string Action { get; set; } = string.Empty;

        public Guid UserId { get; set; }

        public string Description { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}