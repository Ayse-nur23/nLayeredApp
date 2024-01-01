using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.EntityConfiguration;

public class CustomerDemographicConfiguration : IEntityTypeConfiguration<CustomerDemographic>
{
    public void Configure(EntityTypeBuilder<CustomerDemographic> builder)
    {
        builder.ToTable("CustomerDemographics").HasKey(b => b.Id);
        builder.Property(b => b.Id).HasColumnName("CustomerTypeID").IsRequired();
        builder.Property(b => b.CustomerDesc).HasColumnName("CustomerDesc");

        builder.HasMany(b => b.Customers);
        builder.HasQueryFilter(b => !b.DeletedDate.HasValue);
    }
}