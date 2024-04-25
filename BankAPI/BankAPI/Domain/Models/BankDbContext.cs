using Microsoft.EntityFrameworkCore;
using MongoDB.Bson.IO;
using SharpCompress.Common;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json;

namespace BankAPI.Domain.Models
{
    public class BankDbContext : DbContext
    {
        public DbSet<Banks> Banks { get; set; }
        public DbSet<Cards> Cards { get; set; }

        public BankDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Banks>(entity =>
            {
                entity.Property(j => j.Name).HasElementName("name");
                entity.Property(j => j.Code).HasElementName("code");
                entity.Property(j => j.Status).HasElementName("status");
                entity.Property(j => j.Country).HasElementName("country");
            });

            modelBuilder.Entity<Cards>(entity =>
            {
                entity.Property(j => j.CardType).HasElementName("card_type");
                entity.Property(j => j.NameOnCard).HasElementName("name_on_card");
                entity.Property(j => j.Provider).HasElementName("provider");
                entity.Property(j => j.CardNumber).HasElementName("card_number");
                entity.Property(j => j.AccountType).HasElementName("account_type");
                entity.Property(j => j.CVV).HasElementName("cvc");
                entity.Property(j => j.ExpirationDate).HasElementName("expiration_date");
                entity.Property(j => j.Balance).HasElementName("balance");
                entity.Property(j => j.Balance).HasElementName("balance");
                entity.Property(j => j.CreatedAt).HasElementName("created_at");
                entity.Property(j => j.ActivatedAt).HasElementName("activated_at");
                entity.Property(j => j.UserId).HasElementName("user_id");
                entity.Property(j => j.Status).HasElementName("status");

            });
        }
    }
}
