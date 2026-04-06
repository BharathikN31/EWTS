namespace EWTS.Application.DTOs.Task
{
    public class CreateTaskDto
    {
        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public Guid AssignedToUserId { get; set; }
    }
}