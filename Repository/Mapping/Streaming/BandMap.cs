using Domain.Streaming.Agreggates;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Repository.Mapping.Streaming
{ 
    public class BandMap : IEntityTypeConfiguration<Band>
    {
        public void Configure(EntityTypeBuilder<Band> builder)
        {
            builder.ToTable(nameof(Band));

            builder.HasKey(band => band.Id);
            builder.Property(band => band.Id).ValueGeneratedOnAdd();
            builder.Property(band => band.Name).IsRequired().HasMaxLength(50);
            builder.Property(band => band.Description).IsRequired();
            builder.Property(band => band.Backdrop).IsRequired();
            builder.HasMany<Album>(band => band.Albums).WithOne().OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(band => band.Genres).WithMany(genre => genre.Bands);
        }
    }
}