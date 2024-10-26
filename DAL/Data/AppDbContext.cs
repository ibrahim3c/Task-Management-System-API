using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL.Data
{
    public class AppDbContext:DbContext
    {
        public DbSet<Project> Projects { get; set; }

        public DbSet<ProjectTask> Tasks { get; set; }

        public DbSet<TaskAttachment> TaskAttachments { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProjectTask>()
                .HasOne(p => p.Project)
                .WithMany(t => t.Tasks);

            modelBuilder.Entity<TaskAttachment>()
                .HasOne(t => t.Task)
                .WithMany(a => a.Attachments);

            modelBuilder.Entity<TaskAttachment>().HasKey(a => a.AttachmentId);

            base.OnModelCreating(modelBuilder);

            
        }
    }
}
