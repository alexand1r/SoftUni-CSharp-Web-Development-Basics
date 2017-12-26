namespace _5._6._7._8._9._Shop_Hierarchy
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Item
    {
        public Item()
        {
            this.Reviews = new List<Review>();
            this.Orders = new List<ItemsOrders>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        public decimal Price { get; set; }

        public List<Review> Reviews { get; set; }
        public List<ItemsOrders> Orders { get; set; }
    }
}
