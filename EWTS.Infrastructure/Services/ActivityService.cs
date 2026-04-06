using EWTS.Application.Interfaces;
using EWTS.Domain.Entities;
using EWTS.Infrastructure.Persistence;

namespace EWTS.Infrastructure.Services
{
    public class ActivityService : IActivityService
    {
        private readonly AppDbContext _context;

        public ActivityService(AppDbContext context)
        {
            _context = context;
        }

        public async Task LogAsync(Guid userId, string action, string description)
        {
            var log = new ActivityLog
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                Action = action,
                Description = description
            };

            await _context.ActivityLogs.AddAsync(log);
            await _context.SaveChangesAsync();
        }

        public async Task LogActivityAsync(Guid userId, string action, string description)
{
    Console.WriteLine("🔥 Activity Log Triggered");

    var log = new ActivityLog
    {
        Id = Guid.NewGuid(),
        UserId = userId,
        Action = action,
        Description = description
    };

    await _context.ActivityLogs.AddAsync(log);
    await _context.SaveChangesAsync();
}
    }
}