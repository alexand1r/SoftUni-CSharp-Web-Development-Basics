namespace StudentSystem
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.EntityFrameworkCore;
    using StudentSystem.Models;

    class Program
    {
        private static Random random = new Random();

        static void Main(string[] args)
        {
            using (var db = new StudentSystemContext())
            {
                db.Database.Migrate();
                SeedData(db);

                //1.
                //PrintStudentsWithHomeworks(db);

                //2.
                //PrintCoursesWithResources(db);

                //3.
                //PrintCoursesWithMoreThan5Resources(db);

                //4. The Format For The Date is: xxxx(year)-xx(month)-xx(day)
                //PrintCoursesInfoForADate(db);

                //5.
                //PrintStudentsInfo(db);

                //1.
                //PrintCoursesWithResourcesAndLicenses(db);

                //2.
                //PrintStudentsInfoWithResources(db);
            }
        }

        private static void PrintStudentsInfoWithResources(StudentSystemContext db)
        {
            var students = db
                .Students
                .Select(s => new
                {
                    s.Name,
                    Courses = s.Courses.Count,
                    Resources = s.Courses.Sum(c => c.Course.Resources.Count),
                    Licenses = s.Courses.Sum(c => c.Course.Resources.Sum(r => r.Licenses.Count))
                })
                .OrderByDescending(s => s.Courses)
                .ThenByDescending(s => s.Resources)
                .ThenBy(s => s.Name)
                .ToList();

            foreach (var s in students)
            {
                Console.WriteLine($"Student: {s.Name}");
                Console.WriteLine($"Courses: {s.Courses}");
                Console.WriteLine($"Resources: {s.Resources}");
                Console.WriteLine($"Licenses: {s.Licenses}");
                Console.WriteLine();
            }
        }

        private static void PrintCoursesWithResourcesAndLicenses(StudentSystemContext db)
        {
            var courses = db
                .Courses
                .Select(c => new
                {
                    c.Name,
                    ResourcesCount = c.Resources.Count,
                    Resources = c.Resources.Select(r => new
                        {
                            r.Name,
                            LicensesCount = r.Licenses.Count,
                            Licenses = r.Licenses.Select(l => new
                            {
                                l.Name
                            })
                        })
                        .OrderByDescending(r => r.LicensesCount)
                        .ThenBy(r => r.Name)
                })
                .OrderByDescending(c => c.ResourcesCount)
                .ThenBy(c => c.Name)
                .ToList();

            foreach (var c in courses)
            {
                Console.WriteLine($"Course: {c.Name}");
                foreach (var r in c.Resources)
                {
                    Console.WriteLine($"--Resource: {r.Name}");
                    if (r.LicensesCount > 0)
                    {
                        foreach (var l in r.Licenses)
                        {
                            Console.WriteLine($"***License: {l.Name}");
                        }
                    }
                }
                Console.WriteLine();
            }
        }

        private static void PrintStudentsInfo(StudentSystemContext db)
        {
            var students = db
                .Students
                .Where(s => s.Courses.Count > 0)
                .Select(s => new
                {
                    s.Name,
                    CoursesCount = s.Courses.Count,
                    TotalPrice = s.Courses.Sum(c => c.Course.Price),
                    AvgPrice = s.Courses.Sum(c => c.Course.Price) / s.Courses.Count
                })
                .OrderByDescending(s => s.TotalPrice)
                .ThenByDescending(s => s.CoursesCount)
                .ThenBy(s => s.Name)
                .ToList();

            foreach (var s in students)
            {
                Console.WriteLine($"Student: {s.Name}");
                Console.WriteLine($"Courses: {s.CoursesCount}");
                Console.WriteLine($"Total Price: {s.TotalPrice:F2}");
                Console.WriteLine($"Avg Price: {s.AvgPrice:F2}");
                Console.WriteLine();
            }
        }

        private static void PrintCoursesInfoForADate(StudentSystemContext db)
        {
            var dateString = Console.ReadLine();
            var date = Convert.ToDateTime(dateString);
            var courses = db.Courses
                .Where(c => c.StartDate < date && c.EndDate > date)
                .Select(c => new
                {
                    c.Name,
                    c.StartDate,
                    c.EndDate,
                    Duration = c.EndDate.Subtract(c.StartDate).TotalDays,
                    Students = c.Students.Count
                })
                .OrderByDescending(c => c.Students)
                .ThenByDescending(c => c.Duration)
                .ToList();

            foreach (var course in courses)
            {
                Console.WriteLine($"Course: {course.Name}");
                Console.WriteLine($"Start Date: {course.StartDate}");
                Console.WriteLine($"End Date: {course.EndDate}");
                Console.WriteLine($"Duration: {course.Duration}");
                Console.WriteLine($"Students Enrolled: {course.Students}");
                Console.WriteLine();
            }
        }

        private static void PrintCoursesWithMoreThan5Resources(StudentSystemContext db)
        {
            var coursesWithMoreThan5Resources = db
                .Courses
                .Where(c => c.Resources.Count > 5)
                .Select(c => new
                {
                    c.Name,
                    Resources = c.Resources.Count,
                    c.StartDate
                })
                .OrderByDescending(c => c.Resources)
                .ThenByDescending(c => c.StartDate)
                .ToList();

            foreach (var cwmt5r in coursesWithMoreThan5Resources)
            {
                Console.WriteLine($"{cwmt5r.Name} - {cwmt5r.Resources} resources");
            }
        }

        private static void PrintCoursesWithResources(StudentSystemContext db)
        {
            var coursesWithResources = db
                .Courses
                .Select(c => new
                {
                    c.Name,
                    c.Description,
                    c.StartDate,
                    c.EndDate,
                    Resources = c
                        .Resources
                        .Select(r => new
                        {
                            r.Name,
                            r.ResourceType,
                            r.Url
                        })
                })
                .OrderBy(c => c.StartDate)
                .ThenByDescending(c => c.EndDate)
                .ToList();

            foreach (var cwr in coursesWithResources)
            {
                Console.WriteLine($"-{cwr.Name} // {cwr.Description}");
                foreach (var resource in cwr.Resources)
                {
                    Console.WriteLine($"-**{resource.Name} / {resource.ResourceType} / {resource.Url}");
                }
                Console.WriteLine("//---------------------------------------------------------//");
            }
        }

        private static void PrintStudentsWithHomeworks(StudentSystemContext db)
        {
            var studentsWithHomeworks = db
                .Students
                .Select(s => new
                {
                    s.Name,
                    Homeworks = s
                        .Homeworks
                        .Select(h => new
                        {
                            h.Content,
                            h.ContentType
                        })
                }).ToList();

            foreach (var swh in studentsWithHomeworks)
            {
                Console.WriteLine($"-{swh.Name}");
                foreach (var homework in swh.Homeworks)
                {
                    Console.WriteLine($"-**{homework.Content} / {homework.ContentType}");
                }
                Console.WriteLine("//---------------------------------------------------------//");
            }
        }

        private static void SeedData(StudentSystemContext db)
        {
            const int maxStudents = 25;
            const int maxCourses = 10;
            var currentDate = DateTime.Now;

            // Add Students
            Console.Write("Adding Students");

            for (int i = 0; i < maxStudents; i++)
            {
                db.Students.Add(new Student
                {
                    Name = $"Student {i}",
                    RegistrationDate = currentDate.AddDays(i),
                    BirthDay = currentDate.AddYears(-20).AddDays(i),
                    PhoneNumber = $"Random Phone {i}"
                });

                if (i % 5 == 0)
                    Console.Write(".");
            }

            Console.WriteLine();
            db.SaveChanges();

            // Add Courses
            Console.Write("Adding Courses");

            var addedCourses = new List<Course>();

            for (int i = 0; i < maxCourses; i++)
            {
                var course = new Course
                {
                    Name = $"Course {i}",
                    Description = $"Course Details {i}",
                    Price = 100 * i,
                    StartDate = currentDate.AddDays(i),
                    EndDate = currentDate.AddDays(20 + i)
                };

                addedCourses.Add(course);

                db.Courses.Add(course);

                if (i % 2 == 0)
                    Console.Write(".");
            }

            Console.WriteLine();
            db.SaveChanges();

            // Add Students to Courses
            Console.Write("Adding Students To Courses");

            var studentIds = db
                .Students
                .Select(s => s.Id)
                .ToList();

            for (int i = 0; i < maxCourses; i++)
            {
                var currentCourse = addedCourses[i];
                var studentsInCourse = random.Next(2, maxStudents / 2);
                var addedStudents = new List<int>();

                for (int j = 0; j < studentsInCourse; j++)
                {
                    var studentId = studentIds[random.Next(0, studentIds.Count)];

                    while (addedStudents.Contains(studentId))
                    {
                        studentId = studentIds[random.Next(0, studentIds.Count)];
                    }

                    addedStudents.Add(studentId);

                    currentCourse.Students.Add(new StudentCourses
                    {
                        StudentId = studentId
                    });
                    
                    if (i % 2 == 0)
                        Console.Write(".");
                }

                var resourceInCourse = random.Next(2, 20);
                var types = new[] { 0, 1, 2, 999 };

                for (int j = 0; j < resourceInCourse; j++)
                {
                    var resource = new Resource
                    {
                        Name = $"Resource {j}",
                        Url = $"Url {j}",
                        ResourceType = (ResourceTypes) types[random.Next(0, types.Length)]
                    };

                    var licensesForResource = random.Next(2, 5);
                    
                    for (int l = 0; l < licensesForResource; l++)
                    {
                        resource.Licenses.Add(new License
                            {
                                Name = $"Licenes {l}"
                            });
                    }
                    currentCourse.Resources.Add(resource);

                }
            }

            Console.WriteLine();
            db.SaveChanges();

            //Add Homeworks
            for (int i = 0; i < maxCourses; i++)
            {
                var currentCourse = addedCourses[i];

                var studentsInCourse = currentCourse
                    .Students
                    .Select(s => s.StudentId)
                    .ToList();

                for (int j = 0; j < studentsInCourse.Count; j++)
                {
                    var totalHomeworks = random.Next(2, 5);

                    for (int k = 0; k < totalHomeworks; k++)
                    {
                        db.Homeworks.Add(new Homework
                        {
                            Content = $"Homework Content {j}",
                            SubmissionDate = currentDate.AddDays(-j),
                            ContentType = ContentTypes.Zip,
                            StudentId = studentsInCourse[j],
                            CourseId = currentCourse.Id
                        });
                    }
                }

                db.SaveChanges();
            }

        }
    }
}
