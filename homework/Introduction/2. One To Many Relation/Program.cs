using _2._One_To_Many_Relation;
using System;

namespace _2.One_To_Many_Relation
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var db = new MyDbContext())
            {
                db.Database.EnsureDeleted();
                db.Database.EnsureCreated();

                var department = new Department { Name = "Test" };
                department.Employees.Add(new Employee { Name = "Pesho" });
                department.Employees.Add(new Employee { Name = "Gosho", ManagerId = 1 });
                db.Departments.Add(department);
                db.SaveChanges();
            }
        }
    }
}