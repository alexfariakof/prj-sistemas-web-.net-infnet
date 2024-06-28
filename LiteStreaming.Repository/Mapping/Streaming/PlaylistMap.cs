using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Domain.Streaming.Agreggates;
using Repository.Abstractions;

namespace Repository.Mapping.Streaming;
public class PlaylistMap : IEntityTypeConfiguration<Playlist>
{
    private readonly BaseConstants baseConstants;
    public PlaylistMap(BaseConstants baseConstants) : base()
    {
        this.baseConstants = baseConstants;
    }

    public void Configure(EntityTypeBuilder<Playlist> builder)
    {
        builder.ToTable(nameof(Playlist));
        builder.Property(playlist => playlist.Id).HasColumnType("binary(16)")
            .HasConversion(
            v => v.ToByteArray(),
            v => new Guid(v)
            ).ValueGeneratedOnAdd();
        builder.HasKey(playlist => playlist.Id);
        builder.Property(playlist => playlist.Name).IsRequired().HasMaxLength(50);
        builder.Property(playlist => playlist.Backdrop).IsRequired();

        builder.HasMany(playlist => playlist.Genres).WithMany(playlist => playlist.Playlists);

        builder.HasMany(playlist => playlist.Musics).WithMany(playlist => playlist.Playlists).UsingEntity<Dictionary<string, object>>("MusicPlayList",
            dictonary => dictonary.HasOne<Music>().WithMany(),
            dictonary => dictonary.HasOne<Playlist>().WithMany(),
            dictonary =>
            {
                dictonary.Property<DateTime?>("DtAdded").ValueGeneratedOnAdd().HasDefaultValueSql(baseConstants.CURRENT_DATE);
            });

        builder.HasMany(playlist => playlist.Flats).WithMany(playlist => playlist.Playlists).UsingEntity<Dictionary<string, object>>("FlatPlayList",
            dictonary => dictonary.HasOne<Flat>().WithMany(),
            dictonary => dictonary.HasOne<Playlist>().WithMany(),
            dictonary =>
            {
                dictonary.Property<DateTime?>("DtAdded").ValueGeneratedOnAdd().HasDefaultValueSql(baseConstants.CURRENT_DATE);
            });
    }
}