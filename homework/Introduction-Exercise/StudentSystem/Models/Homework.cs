namespace StudentSystem.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Homework
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Content { get; set; }

        public ContentTypes ContentType { get; set; }

        [Required]
        public DateTime SubmissionDate { get; set; }

        public int StudentId { get; set; }

        public Student Student { get; set; }

        public int CourseId { get; set; }

        public Course Course { get; set; }
    }
}
