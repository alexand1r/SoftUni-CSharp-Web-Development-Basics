namespace FootballBetting.Models
{
    using System.ComponentModel.DataAnnotations;

    public class ResultPrediction
    {
        [Key]
        public int Id { get; set; }

        public Prediction Prediction { get; set; }
    }
}
