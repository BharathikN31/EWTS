using EWTS.Domain.Entities;
using EWTS.Domain.Interfaces;
using EWTS.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace EWTS.Infrastructure.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly AppDbContext _context;

        public TaskRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<TaskItem> CreateAsync(TaskItem task)
        {
            _context.Tasks.Add(task);
            await _context.SaveChangesAsync();
            return task;
        }

        public async Task<List<TaskItem>> GetAllAsync()
        {
            return await _context.Tasks.Include(t => t.AssignedToUser).ToListAsync();
        }

        public async Task<TaskItem?> GetByIdAsync(Guid id)
        {
            return await _context.Tasks.Include(t => t.AssignedToUser)
                .FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task UpdateAsync(TaskItem task)
        {
            _context.Tasks.Update(task);
            await _context.SaveChangesAsync();
        }
        public async Task AddApprovalAsync(Approval approval)
        {
            await _context.Approvals.AddAsync(approval);
            await _context.SaveChangesAsync();
        }
    }
}