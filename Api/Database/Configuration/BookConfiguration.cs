using Domain;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Api.Database.Configuration;

public class BookConfiguration : IEntityTypeConfiguration<Book>
{
    public void Configure(EntityTypeBuilder<Book> builder)
    {
        builder.HasKey(t => t.Id);
        builder.Property(p => p.Description)
            .HasMaxLength(200)
            .IsRequired();
        builder.Property(p => p.Authors);
        builder.Property(p => p.Publisher);
        builder.Property(p => p.PageCount).IsRequired();
        builder.Property(p => p.ThumbnailSmallLink);
        builder.Property(p => p.ThumbnailLink);
        builder.Property(p => p.BookCode);
        builder.Property(p => p.Isbn).IsRequired();
        builder.HasOne<BookCase>(p => p.BookCase)
            .WithMany(b => b.Books)
            .HasForeignKey(b => b.BookCaseId);

    }
}