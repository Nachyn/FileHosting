using FileHosting.Storage.AppCore.Entities.FolderAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FileHosting.Storage.Infrastructure.Data.Config;

public class FolderConfiguration : IEntityTypeConfiguration<Folder>
{
    public void Configure(EntityTypeBuilder<Folder> builder)
    {
        var navigation = builder.Metadata.FindNavigation(nameof(Folder.Items));
        navigation?.SetPropertyAccessMode(PropertyAccessMode.Field);

        builder.ToTable("Folders");

        builder.Property(f => f.Name)
            .IsRequired();

        builder.HasOne(f => f.User)
            .WithMany()
            .HasForeignKey(f => f.UserId);
    }
}