using DemoMVC.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace DemoMVC.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Course> Courses { get; set; } = default!;
        public DbSet<Batch> Batches { get; set; }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<Trainee> Trainees { get; set; }
        public DbSet<Registration> Registrations { get; set; }
        public DbSet<Attendance> Attendances { get; set; }
        public DbSet<Evaluation> Evaluations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Đảm bảo mỗi học viên chỉ có 1 bản đánh giá cho mỗi khóa học
            modelBuilder.Entity<Evaluation>()
                .HasIndex(e => new { e.TraineeId, e.CourseId })
                .IsUnique();

            // Đảm bảo mỗi học viên chỉ đăng ký 1 lần cho 1 khóa học
            modelBuilder.Entity<Registration>()
                .HasIndex(r => new { r.TraineeId, r.CourseId })
                .IsUnique();

            // Điểm danh: 1 học viên chỉ có 1 record điểm danh cho mỗi buổi
            modelBuilder.Entity<Attendance>()
                .HasIndex(a => new { a.SessionId, a.TraineeId })
                .IsUnique();

            // Quan hệ giữa Course và Batch (1-n)
            modelBuilder.Entity<Batch>()
                .HasOne(b => b.Course)
                .WithMany(c => c.Batches)
                .HasForeignKey(b => b.CourseId)
                .OnDelete(DeleteBehavior.Cascade);

            // Quan hệ giữa Batch và Session (1-n)
            modelBuilder.Entity<Session>()
                .HasOne(s => s.Batch)
                .WithMany(b => b.Sessions)
                .HasForeignKey(s => s.BatchId)
                .OnDelete(DeleteBehavior.Cascade);

            // Quan hệ giữa Trainee và Registration (1-n)
            modelBuilder.Entity<Registration>()
                .HasOne(r => r.Trainee)
                .WithMany(t => t.Registrations)
                .HasForeignKey(r => r.TraineeId)
                .OnDelete(DeleteBehavior.Cascade);

            // Quan hệ giữa Course và Registration (1-n)
            modelBuilder.Entity<Registration>()
                .HasOne(r => r.Course)
                .WithMany(c => c.Registrations)
                .HasForeignKey(r => r.CourseId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}