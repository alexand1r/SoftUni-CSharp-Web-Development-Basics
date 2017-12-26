namespace _4._Many_To_Many_Relation
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Student
    {
        public Student()
        {
            this.Courses = new List<StudentsCourses>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        
        public List<StudentsCourses> Courses { get; set; }
    }
}
