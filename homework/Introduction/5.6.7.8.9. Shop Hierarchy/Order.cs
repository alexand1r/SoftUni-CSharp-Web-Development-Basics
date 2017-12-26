namespace _5._6._7._8._9._Shop_Hierarchy
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Order
    {
        public Order()
        {
            this.Items = new List<ItemsOrders>();
        }

        [Key]
        public int Id { get; set; }

        public int CustomerId { get; set; }

        public Customer Customer { get; set; }

        public List<ItemsOrders> Items { get; set; }
    }
}
