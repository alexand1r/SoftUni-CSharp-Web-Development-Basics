namespace _4._Many_To_Many_Relation
{
    using Microsoft.EntityFrameworkCore;

    class MyDbContext : DbContext
    {
        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=.\SQLEXPRESS;Database=Test2Db;Integrated Security=True;");

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<StudentsCourses>()
                .HasKey(sc => new { sc.StudentId, sc.CourseId });

            modelBuilder.Entity<Student>()
                 .HasMany(st => st.Courses)
                 .WithOne(c => c.Student)
                 .HasForeignKey(c => c.StudentId);

            modelBuilder.Entity<Course>()
                 .HasMany(c => c.Students)
                 .WithOne(st => st.Course)
                 .HasForeignKey(st => st.CourseId);
        }
    }
}
