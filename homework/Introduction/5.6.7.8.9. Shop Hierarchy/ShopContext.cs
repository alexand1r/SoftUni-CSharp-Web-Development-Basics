namespace _5._6._7._8._9._Shop_Hierarchy
{
    using Microsoft.EntityFrameworkCore;

    class ShopContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Salesmen> Salesmens { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Item> Items { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=.\SQLEXPRESS;Database=ShopHierarchy;Integrated Security=True;");

            base.OnConfiguring(optionsBuilder);
        }
   
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>()
                .HasOne<Salesmen>(c => c.Salesmen)
                .WithMany(s => s.Customers)
                .HasForeignKey(c => c.SalesmenId);

            modelBuilder.Entity<Order>()
                .HasOne<Customer>(o => o.Customer)
                .WithMany(c => c.Orders)
                .HasForeignKey(o => o.CustomerId);

            modelBuilder.Entity<Review>()
                .HasOne<Customer>(r => r.Customer)
                .WithMany(c => c.Reviews)
                .HasForeignKey(r => r.CustomerId);

            modelBuilder.Entity<Review>()
                .HasOne<Item>(r => r.Item)
                .WithMany(i => i.Reviews)
                .HasForeignKey(r => r.ItemId);

            modelBuilder.Entity<ItemsOrders>()
                .HasKey(io => new { io.ItemId, io.OrderId });

            modelBuilder.Entity<Item>()
                .HasMany(i => i.Orders)
                .WithOne(o => o.Item)
                .HasForeignKey(io => io.ItemId);

            modelBuilder.Entity<Order>()
                .HasMany(o => o.Items)
                .WithOne(i => i.Order)
                .HasForeignKey(io => io.OrderId);
        }
    }
}
