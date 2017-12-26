namespace FootballBetting.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Competition
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public int CompetitionTypeId { get; set; }

        public CompetitionType CompetitionType { get; set; }

        public List<Game> Games { get; set; } = new List<Game>();
    }
}
