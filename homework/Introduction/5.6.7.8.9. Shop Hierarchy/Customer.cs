namespace _5._6._7._8._9._Shop_Hierarchy
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Customer
    {
        public Customer()
        {
            this.Reviews = new List<Review>();
            this.Orders = new List<Order>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        public int SalesmenId { get; set; }

        public Salesmen Salesmen { get; set; }

        public List<Review> Reviews { get; set; }

        public List<Order> Orders { get; set; }
    }
}
