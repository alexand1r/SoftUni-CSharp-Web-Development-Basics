namespace StudentSystem.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Student
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }    

        public string PhoneNumber { get; set; }

        [Required]
        public DateTime RegistrationDate { get; set; }

        public DateTime? BirthDay { get; set; }

        public List<StudentCourses> Courses { get; set; } = new List<StudentCourses>();

        public List<Homework> Homeworks { get; set; } = new List<Homework>();
    }
}
