namespace FootballBetting.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Continent
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public List<CountriesContinents> Countries { get; set; } = new List<CountriesContinents>();
    }
}
