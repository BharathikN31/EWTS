using Microsoft.EntityFrameworkCore;
using EWTS.Domain.Entities;
namespace EWTS.Infrastructure.Persistence
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<User> Users { get; set; }
        public DbSet<TaskItem> Tasks { get; set; }
        public DbSet<Approval> Approvals { get; set; }
        public DbSet<ActivityLog> ActivityLogs { get; set; }


        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<TaskItem>()
    .HasOne(t => t.AssignedToUser)
    .WithMany()
    .HasForeignKey(t => t.AssignedToUserId)
    .OnDelete(DeleteBehavior.Restrict);

modelBuilder.Entity<Approval>()
    .HasOne(a => a.Task)
    .WithMany()
    .HasForeignKey(a => a.TaskId)
    .OnDelete(DeleteBehavior.Restrict);
        }
    }
}