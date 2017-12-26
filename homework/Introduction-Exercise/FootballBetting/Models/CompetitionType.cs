namespace FootballBetting.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class CompetitionType
    {
        [Key]
        public int Id { get; set; }

        public CompetitionTypes Type { get; set; }

        public List<Competition> Competitions { get; set; } = new List<Competition>();
    }
}
