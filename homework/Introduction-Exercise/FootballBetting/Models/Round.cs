namespace FootballBetting.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Round
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public List<Game> Games { get; set; } = new List<Game>();
    }
}
