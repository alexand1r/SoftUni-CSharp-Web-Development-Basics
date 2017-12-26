namespace _5._6._7._8._9._Shop_Hierarchy
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Salesmen
    {
        public Salesmen()
        {
            this.Customers = new List<Customer>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        public List<Customer> Customers { get; set; }
    }
}
