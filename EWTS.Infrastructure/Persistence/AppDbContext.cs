using Microsoft.EntityFrameworkCore;
using EWTS.Domain.Entities;
using Microsoft.Identity.Client;
namespace EWTS.Infrastructure.Persistence
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions <AppDbContext> options) : base(options)
        {
            
        }
        public DbSet<User> Users {get; set;}
    }
}