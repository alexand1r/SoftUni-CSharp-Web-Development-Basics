namespace FootballBetting.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Bet
    {
        [Key]
        public int Id { get; set; }

        public int BetMoney { get; set; }

        public DateTime BetDateAndTime { get; set; }

        public int UserId { get; set; }

        public User User { get; set; }

        public List<BetGame> Games { get; set; } = new List<BetGame>();
    }
}
