using Domain;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Api.Database.Configuration;

public class BookCaseConfiguration : IEntityTypeConfiguration<BookCase>
{
    public void Configure(EntityTypeBuilder<BookCase> builder)
    {
        builder.HasKey(t => t.Id);
        builder.Property(p => p.Label)
            .HasMaxLength(50)
            .IsRequired();
        builder.HasOne<BookOwner>(b => b.Owner);
        builder.HasMany(b => b.Books);
    }
}