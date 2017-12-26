namespace FootballBetting.Models
{
    using Microsoft.EntityFrameworkCore;
    public class FootballBettingContext : DbContext
    {
        public DbSet<Player> Players { get; set; }
        public DbSet<Bet> Bets { get; set; }
        public DbSet<Color> Colors { get; set; }
        public DbSet<Competition> Competitions { get; set; }
        public DbSet<CompetitionType> CompetitionTypes { get; set; }
        public DbSet<Continent> Continents { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Town> Towns { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<ResultPrediction> ResultPredictions { get; set; }
        public DbSet<Round> Rounds { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=.\SQLEXPRESS;Database=FootballBetting;Integrated Security=True;");
            
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PlayerStatistics>()
                .HasKey(ps => new {ps.GameId, ps.PlayerId});

            modelBuilder.Entity<Player>()
                .HasMany(p => p.Games)
                .WithOne(g => g.Player)
                .HasForeignKey(ps => ps.PlayerId);

            modelBuilder.Entity<Game>()
                .HasMany(g => g.Players)
                .WithOne(p => p.Game)
                .HasForeignKey(ps => ps.GameId);

            modelBuilder.Entity<BetGame>()
                .HasKey(bg => new {bg.GameId, bg.BetId});

            modelBuilder.Entity<Game>()
                .HasMany(g => g.Bets)
                .WithOne(b => b.Game)
                .HasForeignKey(bg => bg.GameId);

            modelBuilder.Entity<Bet>()
                .HasMany(b => b.Games)
                .WithOne(g => g.Bet)
                .HasForeignKey(bg => bg.BetId);

            modelBuilder.Entity<CountriesContinents>()
                .HasKey(cc => new {cc.CountryId, cc.ContinentId});

            modelBuilder.Entity<Country>()
                .HasMany(c => c.Continents)
                .WithOne(c => c.Country)
                .HasForeignKey(cc => cc.CountryId);

            modelBuilder.Entity<Continent>()
                .HasMany(c => c.Countries)
                .WithOne(c => c.Continent)
                .HasForeignKey(cc => cc.ContinentId);

            modelBuilder.Entity<Team>()
                .HasOne<Town>(t => t.Town)
                .WithMany(t => t.Teams)
                .HasForeignKey(t => t.TownId);

            modelBuilder.Entity<Town>()
                .HasOne<Country>(t => t.Country)
                .WithMany(c => c.Towns)
                .HasForeignKey(t => t.CountryId);

            modelBuilder.Entity<Player>()
                .HasOne<Team>(p => p.Team)
                .WithMany(t => t.Players)
                .HasForeignKey(p => p.TeamId);

            modelBuilder.Entity<Player>()
                .HasOne<Position>(p => p.Position)
                .WithMany(p => p.Players)
                .HasForeignKey(p => p.PositionId);

            modelBuilder.Entity<Game>()
                .HasOne<Round>(g => g.Round)
                .WithMany(r => r.Games)
                .HasForeignKey(g => g.RoundId);

            modelBuilder.Entity<Game>()
                .HasOne<Competition>(g => g.Competition)
                .WithMany(c => c.Games)
                .HasForeignKey(g => g.CompetitionId);

            modelBuilder.Entity<Bet>()
                .HasOne<User>(b => b.User)
                .WithMany(u => u.Bets)
                .HasForeignKey(b => b.UserId);

            modelBuilder.Entity<Game>()
                .HasOne<Team>(g => g.AwayTeam)
                .WithMany(t => t.AwayGames)
                .HasForeignKey(g => g.AwayTeamId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Game>()
                .HasOne<Team>(g => g.HomeTeam)
                .WithMany(t => t.HomeGames)
                .HasForeignKey(g => g.HomeTeamId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Competition>()
                .HasOne<CompetitionType>(c => c.CompetitionType)
                .WithMany(c => c.Competitions)
                .HasForeignKey(c => c.CompetitionTypeId);

            modelBuilder.Entity<Team>()
                .HasOne<Color>(t => t.PrimaryKitColor)
                .WithMany()
                .HasForeignKey(t => t.PrimaryKitColorId);

            modelBuilder.Entity<Team>()
                .HasOne<Color>(t => t.SecondaryKitColor)
                .WithMany()
                .HasForeignKey(t => t.SecondaryKitColorId);

            base.OnModelCreating(modelBuilder);
        }
    }
}
