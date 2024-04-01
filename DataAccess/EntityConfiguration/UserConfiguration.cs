using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.EntityConfiguration;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users").HasKey(b => b.Id);
        builder.Property(ul => ul.Id).HasColumnName("Id").IsRequired();
        builder.Property(ul => ul.FirstName).HasColumnName("FirstName");
        builder.Property(ul => ul.LastName).HasColumnName("LastName");
        builder.Property(b => b.Email).HasColumnName("Email");
        builder.Property(ul => ul.PasswordSalt).HasColumnName("PasswordSalt");
        builder.Property(ul => ul.PasswordHash).HasColumnName("PasswordHash");


        builder.HasQueryFilter(ul => !ul.DeletedDate.HasValue);
    }
}
