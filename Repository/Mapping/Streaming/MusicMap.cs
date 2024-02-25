using Domain.Streaming.Agreggates;
using Domain.Streaming.ValueObject;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.Mapping.Streaming;
public class MusicMap : IEntityTypeConfiguration<Music<Playlist>>
{
    public void Configure(EntityTypeBuilder<Music<Playlist>> builder)
    {
        builder.ToTable("Music");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedOnAdd();
        builder.Property(x => x.Name).IsRequired().HasMaxLength(50);

        builder.OwnsOne<Duration>(d => d.Duration, c =>
        {
            c.Property(x => x.Value).HasColumnName("Duration").IsRequired().HasMaxLength(50);
        });

        builder.HasMany(x => x.Playlists).WithMany(m => m.Musics);
    }
}
