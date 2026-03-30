using CrepeDuChef.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

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
                string path =
                    Path.Combine(
                        Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                        "CrepeDuChef.db");
                
                string connection = $"Data Source={path}";
                Debug.WriteLine($"Connection string for db is : {connection}");
                
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

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(u => u.FirstName)
                      .IsRequired()
                      .HasMaxLength(100);

                entity.Property(u => u.LastName)
                      .IsRequired()
                      .HasMaxLength(100);
            });
        }
    }
}
