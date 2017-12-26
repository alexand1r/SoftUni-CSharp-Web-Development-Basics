namespace _2.One_To_Many_Relation
{
    using _2._One_To_Many_Relation;
    using Microsoft.EntityFrameworkCore;

    class MyDbContext : DbContext
    {
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=.\SQLEXPRESS;Database=TestDb;Integrated Security=True;");

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>()
                .HasOne<Department>(emp => emp.Department)
                .WithMany(d => d.Employees)
                .HasForeignKey(emp => emp.DepartmentId);

            modelBuilder.Entity<Employee>()
                .HasOne<Employee>(emp => emp.Manager)
                .WithMany(m => m.Employees)
                .HasForeignKey(emp => emp.ManagerId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
