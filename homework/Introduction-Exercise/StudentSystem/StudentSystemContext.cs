namespace StudentSystem
{
    using Microsoft.EntityFrameworkCore;
    using StudentSystem.Models;

    public class StudentSystemContext : DbContext
    {
        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Homework> Homeworks { get; set; }
        public DbSet<Resource> Resources { get; set; }
        public DbSet<License> Licenses { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=.\SQLEXPRESS;Database=StudentSystem;Integrated Security=True;");

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<StudentCourses>()
                .HasKey(sc => new {sc.StudentId, sc.CourseId});

            modelBuilder.Entity<Student>()
                .HasMany(s => s.Courses)
                .WithOne(c => c.Student)
                .HasForeignKey(sc => sc.StudentId);

            modelBuilder.Entity<Course>()
                .HasMany(c => c.Students)
                .WithOne(s => s.Course)
                .HasForeignKey(sc => sc.CourseId);

            modelBuilder.Entity<Resource>()
                .HasOne<Course>(r => r.Course)
                .WithMany(c => c.Resources)
                .HasForeignKey(r => r.CourseId);

            modelBuilder.Entity<Homework>()
                .HasOne<Course>(h => h.Course)
                .WithMany(c => c.Homeworks)
                .HasForeignKey(h => h.CourseId);

            modelBuilder.Entity<Homework>()
                .HasOne<Student>(h => h.Student)
                .WithMany(s => s.Homeworks)
                .HasForeignKey(h => h.StudentId);

            modelBuilder.Entity<License>()
                .HasOne<Resource>(l => l.Resource)
                .WithMany(r => r.Licenses)
                .HasForeignKey(l => l.ResourceId);

            base.OnModelCreating(modelBuilder);
        }
    }
}
