namespace _5._6._7._8._9._Shop_Hierarchy
{
    using System.ComponentModel.DataAnnotations;

    public class Review
    {
        public Review()
        {

        }

        [Key]
        public int Id { get; set; }

        public int CustomerId { get; set; }

        public Customer Customer { get; set; }

        public int ItemId { get; set; }

        public Item Item { get; set; }
    }
}
