using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PromoCodeFactory.Core.Domain.PromoCodeManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PromoCodeFactory.DataAccess.Configurations
{
    public class CustomerPreferenceConfiguration : IEntityTypeConfiguration<CustomerPreference>
    {
        public void Configure(EntityTypeBuilder<CustomerPreference> builder)
        {
            builder.HasKey(cp => new { cp.CustomerId, cp.PreferenceId });
            builder.HasOne(cp => cp.Customer)
                .WithMany(p => p.Preferences)
                .HasForeignKey(cp => cp.CustomerId);
            builder.HasOne(cp => cp.Preference)
                .WithMany(p => p.Customers)
                .HasForeignKey(cp => cp.PreferenceId);
        }
    }
}
