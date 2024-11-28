using Microsoft.EntityFrameworkCore;
using TaskManagementAPI.Models;

namespace TaskManagementAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<TaskItem> Tasks { get; set; }
        public DbSet<TaskHistory> TaskHistories { get; set; } // Tabela de histórico

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<TaskHistory>()
                .HasOne(t => t.Task)
                .WithMany()
                .HasForeignKey(t => t.TaskId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}