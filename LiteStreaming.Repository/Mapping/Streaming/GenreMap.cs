using Domain.Streaming.Agreggates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.Mapping.Streaming;
public class GenreMap : IEntityTypeConfiguration<Genre>
{
    public void Configure(EntityTypeBuilder<Genre> builder)
    {
        builder.ToTable("Genre");        
        builder.Property(genre => genre.Id).HasColumnType("binary(16)")
        .HasConversion(
            v => v.ToByteArray(),
            v => new Guid(v)
        )
        .ValueGeneratedOnAdd();
        builder.HasKey(genre => genre.Id);
        builder.Property(genre => genre.Name).IsRequired().HasMaxLength(50);
    }
}