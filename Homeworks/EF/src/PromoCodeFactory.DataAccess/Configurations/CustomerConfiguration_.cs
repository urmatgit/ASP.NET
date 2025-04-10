﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PromoCodeFactory.Core.Domain.Administration;
using PromoCodeFactory.Core.Domain.PromoCodeManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PromoCodeFactory.DataAccess.Configurations
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x=>x.FirstName).IsRequired().HasMaxLength(100);
            builder.Property(x=>x.LastName).IsRequired().HasMaxLength(100);
            builder.Property(x=> x.Email).IsRequired();
            
            builder.HasMany(x => x.PromoCodes).WithOne(x => x.Customer)
                .HasForeignKey(p=>p.CustomerId);
        }
    }
}
