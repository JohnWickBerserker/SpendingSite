namespace SpendingSite.Db
{
    using Entities;
    using Microsoft.EntityFrameworkCore;

    public class AppDbContext : DbContext
    {
        private const string connectionString = "Host=localhost;Database=fin;Username=finadmin;Password=123";

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseNpgsql(connectionString)
                .UseLowerCaseNamingConvention();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("fin");
        }

        public DbSet<Spending> Spendings { get; set; }
        public DbSet<SpendKind> SpendKinds { get; set; }
    }
}
