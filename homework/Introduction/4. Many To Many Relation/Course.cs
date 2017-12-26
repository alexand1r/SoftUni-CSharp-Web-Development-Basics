namespace _4._Many_To_Many_Relation
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Course
    {
        public Course()
        {
            this.Students = new List<StudentsCourses>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        
        public List<StudentsCourses>Students { get; set; }
    }
}
