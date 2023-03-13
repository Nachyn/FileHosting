using FileHosting.Storage.AppCore.Entities.FolderAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FileHosting.Storage.Infrastructure.Data.Config;

public class FolderItemConfiguration : IEntityTypeConfiguration<FolderItem>
{
    public void Configure(EntityTypeBuilder<FolderItem> builder)
    {
        builder.ToTable("FolderItems");

        builder.Property(fi => fi.Name)
            .IsRequired();

        builder.Property(fi => fi.Path)
            .IsRequired();

        builder.HasOne<Folder>()
            .WithMany(f => f.Items)
            .HasForeignKey(fi => fi.FolderId);
    }
}