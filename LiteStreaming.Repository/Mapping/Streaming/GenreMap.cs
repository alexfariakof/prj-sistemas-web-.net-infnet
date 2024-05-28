using Domain.Streaming.Agreggates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.Mapping.Streaming;
public class GenreMap : IEntityTypeConfiguration<Genre>
{
    public void Configure(EntityTypeBuilder<Genre> builder)
    {
        builder.ToTable("Genre");

        builder.HasKey(genre => genre.Id);
        builder.Property(genre => genre.Id).ValueGeneratedOnAdd();
        builder.Property(genre => genre.Name).IsRequired().HasMaxLength(50);
    }
}