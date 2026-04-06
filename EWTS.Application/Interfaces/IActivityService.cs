namespace EWTS.Application.Interfaces
{
    public interface IActivityService
{
    Task LogAsync(Guid userId, string action, string description);
}
}