namespace FootballBetting.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Player
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public int SquadNumber { get; set; }

        public int TeamId { get; set; }

        public Team Team { get; set; }

        public string PositionId { get; set; }

        public Position Position { get; set; }

        public bool IsCurrentlyInjured { get; set; }

        public List<PlayerStatistics> Games { get; set; } = new List<PlayerStatistics>();
    }
}
