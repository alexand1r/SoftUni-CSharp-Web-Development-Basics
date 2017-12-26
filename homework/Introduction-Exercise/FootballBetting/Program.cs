namespace FootballBetting
{
    using FootballBetting.Models;
    using Microsoft.EntityFrameworkCore;

    class Program
    {
        static void Main(string[] args)
        {
            using (var db = new FootballBettingContext())
            {
                db.Database.Migrate();
                
            }
        }
    }
}
