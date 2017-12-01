using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using LearningSystem.Data.Models;

namespace LearningSystem.Data
{
    public class LearningSystemDbContext : IdentityDbContext<User>
    {
        public DbSet<Article> Articles { get; set; }

        public DbSet<Course> Courses { get; set; }

        public DbSet<UserCourse> UsersCourses { get; set; }

        public LearningSystemDbContext(DbContextOptions<LearningSystemDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<UserCourse>()
                .HasKey(uc => new { uc.UserId, uc.CourseId });

            builder.Entity<User>()
                .HasMany(u => u.Articles)
                .WithOne(a => a.Author)
                .HasForeignKey(a => a.AuthorId);

            builder.Entity<User>()
                .HasMany(u => u.Courses)
                .WithOne(c => c.User)
                .HasForeignKey(c => c.UserId);

            builder.Entity<Course>()
                .HasMany(c => c.Users)
                .WithOne(u => u.Course)
                .HasForeignKey(c => c.CourseId);

            base.OnModelCreating(builder);
        }
    }
}
