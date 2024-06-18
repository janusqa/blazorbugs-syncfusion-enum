using Microsoft.EntityFrameworkCore;
using EnumApp.Models;

namespace EnumApp.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Notification> Notifications { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Notification>().HasData(
              new Notification { Id = 1, Level = NotificationType.ERROR },
              new Notification { Id = 2, Level = NotificationType.SUCCESS },
              new Notification { Id = 3, Level = NotificationType.INFORMATION },
              new Notification { Id = 4, Level = NotificationType.WARNING }
            );
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableDetailedErrors().EnableSensitiveDataLogging();
        }
    }
}