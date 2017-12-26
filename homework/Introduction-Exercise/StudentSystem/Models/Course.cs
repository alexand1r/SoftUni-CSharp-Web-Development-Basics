namespace StudentSystem.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Course
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [Required]
        public decimal Price { get; set; }

        public List<StudentCourses> Students { get; set; } = new List<StudentCourses>();

        public List<Resource> Resources { get; set; } = new List<Resource>();

        public List<Homework> Homeworks { get; set; } = new List<Homework>();
    }
}
