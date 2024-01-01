using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.EntityConfiguration;

public class CustomerCustomerDemoConfiguration : IEntityTypeConfiguration<CustomerCustomerDemo>
{
    public void Configure(EntityTypeBuilder<CustomerCustomerDemo> builder)
    {
        builder.ToTable("CustomerCustomerDemo").HasKey(b => new {b.Id, b.CustomerTypeID});
        builder.Property(b => b.Id).HasColumnName("CustomerID").IsRequired();
        builder.Property(b => b.CustomerTypeID).HasColumnName("CustomerTypeID").IsRequired();

        builder.HasOne(b => b.Customer).WithMany(b=>b.CustomerDemographics).HasForeignKey(b=>b.Id);
        builder.HasOne(b => b.CustomerDemographic).WithMany(b => b.Customers).HasForeignKey(b => b.CustomerTypeID);
        builder.HasQueryFilter(b => !b.DeletedDate.HasValue);
    }
}
