using assignment3.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace assignment3.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Major> Majors { get; set; }
        public DbSet<StudentDetails> StudentDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           
            modelBuilder.Entity<Student>()
                .HasOne(s => s.Mentor)
                .WithMany(s => s.Mentees)
                .HasForeignKey(s => s.MentorId)
                .OnDelete(DeleteBehavior.Restrict);

           
            modelBuilder.Entity<Student>()
                .HasOne(s => s.StudentDetails)
                .WithOne(sd => sd.Student)
                .HasForeignKey<StudentDetails>(sd => sd.StudentId);

         
            modelBuilder.Entity<Student>()
                .HasMany(s => s.Courses)
                .WithMany(c => c.Students)
                .UsingEntity(j => j.ToTable("CourseStudent"));
        }
    }
}
