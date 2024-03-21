using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Api.Database.Configuration;

public class BookOwnerConfiguration : IEntityTypeConfiguration<BookOwner>
{
    public void Configure(EntityTypeBuilder<BookOwner> builder)
    {
        builder.HasKey(t => t.Id);
        builder.Property(p => p.UserName)
            .HasMaxLength(15)
            .IsRequired();
    }
}