namespace FootballBetting.Models
{
    public class PlayerStatistics
    {
        public int PlayerId { get; set; }

        public Player Player { get; set; }

        public int GameId { get; set; }

        public Game Game { get; set; }

        public int ScoredGoals { get; set; }

        public int PlayerAssists { get; set; }

        public int PlayedMinitesDuringGame { get; set; }
    }
}
