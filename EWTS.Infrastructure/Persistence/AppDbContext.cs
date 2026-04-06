using Microsoft.EntityFrameworkCore;
using EWTS.Domain.Entities;
using Microsoft.Identity.Client;
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

            modelBuilder.Entity<Approval>()
                .HasOne(a => a.ApprovedByUser)
                .WithMany()
                .HasForeignKey(a => a.ApprovedByUserId)
                .OnDelete(DeleteBehavior.Restrict); // 🔥 FIX HERE
        }
    }
}