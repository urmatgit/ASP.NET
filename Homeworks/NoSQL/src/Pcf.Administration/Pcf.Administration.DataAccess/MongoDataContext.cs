using Microsoft.EntityFrameworkCore;
using MongoDB.EntityFrameworkCore.Extensions;
using Pcf.Administration.Core.Domain.Administration;
using Pcf.Administration.DataAccess.Data;

namespace Pcf.Administration.DataAccess
{
    public class MongoDataContext
        : DbContext
    {
        public DbSet<Role> Roles { get; set; }
        
        public DbSet<Employee> Employees { get; set; }

        public MongoDataContext()
        {
            
        }
        
        public MongoDataContext(DbContextOptions<DataContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Role>().ToCollection("roles");
            modelBuilder.Entity<Employee>().ToCollection("employees");
        }
    }
}