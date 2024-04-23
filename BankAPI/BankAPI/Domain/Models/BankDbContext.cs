using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;

namespace BankAPI.Domain.Models
{
    public class BankDbContext : DbContext
    {
        public DbSet<Banks> Banks { get; set; }
        public BankDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Banks>(j => j.Property(j => j.Name).HasElementName("name"));
            modelBuilder.Entity<Banks>(j => j.Property(j => j.Code).HasElementName("code"));
            modelBuilder.Entity<Banks>(j => j.Property(j => j.Status).HasElementName("status"));
            modelBuilder.Entity<Banks>(j => j.Property(j => j.Country).HasElementName("country"));
        }
    }
}
