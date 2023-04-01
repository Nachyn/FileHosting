using FileHosting.Storage.AppCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FileHosting.Storage.Infrastructure.Data.Config;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");

        builder.Property(u => u.Id).ValueGeneratedNever();

        builder.HasIndex(u => u.UserName)
            .IsUnique();
    }
}