using Microsoft.EntityFrameworkCore;
using TasksManager.Domain.Entities;

namespace TasksManager.Persistence
{
    public class ApplicationDbContext: DbContext
    {
        public DbSet<AppTask> Tasks { get; set; }
        public DbSet<User> Users { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<AppTask>().ToTable("Tasks");
            modelBuilder.Entity<User>().ToTable("Users");

            modelBuilder.Entity<AppTask>()
                .HasKey(task => task.Id);

            modelBuilder.Entity<AppTask>()
                .HasOne(task => task.UserCreated)
                .WithMany(user => user.CreatedTask)
                .HasForeignKey(task => task.IdUserCreated)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<AppTask>()
                .HasOne(task => task.UserAssigned)
                .WithMany(user => user.AssignedTask)
                .HasForeignKey(task => task.IdUserAssigned)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<User>()
                .HasKey(user => user.Id);

            modelBuilder.Entity<User>()
                .HasIndex(user => user.Username)
                .IsUnique();
        }
    }
}
