namespace FootballBetting.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Game
    {
        [Key]
        public int Id { get; set; }

        public int HomeTeamId { get; set; }

        public Team HomeTeam { get; set; }

        public int AwayTeamId { get; set; }

        public Team AwayTeam { get; set; }

        public int HomeGoals { get; set; }

        public int AwayGoals { get; set; }

        public DateTime GameDateAndTime { get; set; }

        public decimal HomeTeamWinBetRate { get; set; }

        public decimal AwayTeamWinBetRate { get; set; }

        public decimal DrawGameBetRate { get; set; }

        public int RoundId { get; set; }

        public Round Round { get; set; }

        public int CompetitionId { get; set; }

        public Competition Competition { get; set; }

        public List<PlayerStatistics> Players { get; set; } = new List<PlayerStatistics>();

        public List<BetGame> Bets { get; set; } = new List<BetGame>();
    }
}
