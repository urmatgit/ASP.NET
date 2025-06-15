using Microsoft.EntityFrameworkCore;
using MongoDB.EntityFrameworkCore.Extensions;
using Pcf.GivingToCustomer.Core.Domain;

namespace Pcf.GivingToCustomer.DataAccess
{
    public class MongoDataContext
        : DbContext
    {
        public DbSet<PromoCode> PromoCodes { get; set; }
        
        public DbSet<Customer> Customeres { get; set; }
        public DbSet<Preference> Preferences { get; set; }
        public MongoDataContext()
        {
            
        }
        
        public MongoDataContext(DbContextOptions<MongoDataContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<PromoCode>().ToCollection("PromoCodes");
            modelBuilder.Entity<Customer>().ToCollection("Customers");
            modelBuilder.Entity<Preference>().ToCollection("Preferences");

        }
    }
}