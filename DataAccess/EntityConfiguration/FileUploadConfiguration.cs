using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.EntityConfiguration;

//Her pairin tek github projesi olacak fakat ödev sistemine bireysel yüklenecek. 

public class FileUploadConfiguration : IEntityTypeConfiguration<FileUpload>
{
    public void Configure(EntityTypeBuilder<FileUpload> builder)
    {
        builder.ToTable("FileUploads").HasKey(b => b.Id);
        builder.Property(b => b.Id).HasColumnName("Id").IsRequired();
        builder.Property(b => b.FileName).HasColumnName("FileName");
        builder.Property(b => b.FilePath).HasColumnName("Destination");
        //builder.Property(b => b.Description).HasColumnName("Description");

       // builder.HasOne(b => b.User);
        builder.HasQueryFilter(b => !b.DeletedDate.HasValue);
    }
}