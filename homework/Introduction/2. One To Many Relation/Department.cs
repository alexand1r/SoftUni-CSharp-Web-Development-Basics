namespace _2._One_To_Many_Relation
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Department
    {
        public Department()
        {
            this.Employees = new List<Employee>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        public List<Employee> Employees { get; set; }
    }
}
