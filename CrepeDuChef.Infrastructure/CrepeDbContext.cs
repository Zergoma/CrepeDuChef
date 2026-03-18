using CrepeDuChef.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace CrepeDuChef.Infrastructure
{
    public class CrepeDbContext : DbContext
    {
        public DbSet<CrepesParty> CrepesParty { get; set; }
        public DbSet<User> Users { get; set; }
        public CrepeDbContext(DbContextOptions<CrepeDbContext> options) : base(options)
        {
        }

        protected CrepeDbContext()
        {
        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                string path = Constants.GetDbPath();
                string connection = $"Data Source={path}";
                Console.WriteLine($"Connection string for db is : {connection}");
                optionsBuilder.UseSqlite(connection);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CrepesParty>(entity =>
            {
                entity.Property(cp => cp.Date)
                      .IsRequired();

                entity.HasOne(cp => cp.User)
                      .WithMany(u => u.CrepesParties)
                      .HasForeignKey(cp => cp.UserId)
                      .IsRequired();

                entity.Property(cp => cp.SessionNumber)
                      .IsRequired();

                entity.HasIndex(cp => new { cp.SessionNumber, cp.UserId })
                      .IsUnique();
            });
        }
    }
}
