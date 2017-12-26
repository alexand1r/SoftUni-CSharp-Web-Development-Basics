namespace BankSystem.Models
{
    using Microsoft.EntityFrameworkCore;

    public class BankSystemContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<SavingAccount> SavingAccounts { get; set; }
        public DbSet<CheckingAccount> CheckingAccounts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=.\SQLEXPRESS;Database=BankSystem;Integrated Security=True;");

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SavingAccount>()
                .HasOne<User>(sa => sa.User)
                .WithMany(u => u.SavingAccounts)
                .HasForeignKey(sa => sa.UserId);

            modelBuilder.Entity<CheckingAccount>()
                .HasOne<User>(ca => ca.User)
                .WithMany(u => u.CheckingAccounts)
                .HasForeignKey(ca => ca.UserId);

            base.OnModelCreating(modelBuilder);
        }
    }
}
