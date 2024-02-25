using Domain.Streaming.Agreggates;
using Domain.Streaming.ValueObject;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Domain.Account.Agreggates;
public class MusicPersonalMap : IEntityTypeConfiguration<Music<PlaylistPersonal>>
{
    public void Configure(EntityTypeBuilder<Music<PlaylistPersonal>> builder)
    {

        builder.ToTable("MusicPersonal");

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
