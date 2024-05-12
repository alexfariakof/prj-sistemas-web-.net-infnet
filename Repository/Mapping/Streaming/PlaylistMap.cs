using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Domain.Streaming.Agreggates;

namespace Repository.Mapping.Streaming
{
    public class PlaylistMap : IEntityTypeConfiguration<Playlist>
    {
        public void Configure(EntityTypeBuilder<Playlist> builder)
        {
            builder.ToTable(nameof(Playlist));

            builder.HasKey(playlist => playlist.Id);
            builder.Property(playlist => playlist.Id).ValueGeneratedOnAdd();
            builder.Property(playlist => playlist.Name).IsRequired().HasMaxLength(50);
            builder.Property(playlist => playlist.Backdrop).IsRequired();
            
            builder.HasMany(playlist => playlist.Genres).WithMany(playlist => playlist.Playlists);

            builder.HasMany(playlist => playlist.Musics)
                    .WithMany(playlist => playlist.Playlists)
                    .UsingEntity<Dictionary<string, object>>(
                    "MusicPlayList",
                    j => j
                        .HasOne<Music>()
                        .WithMany(),
                    j => j
                        .HasOne<Playlist>()
                        .WithMany(),
                    j =>
                    {
                        j.Property<DateTime?>("DtAdded").ValueGeneratedOnAdd();
                    });

            builder.HasMany(playlist => playlist.Flats)
                .WithMany(playlist => playlist.Playlists)
                .UsingEntity<Dictionary<string, object>>(
                "FlatPlayList",
                j => j
                .HasOne<Flat>()
                .WithMany(),
                j => j
                .HasOne<Playlist>()
                .WithMany(),
                j =>
                {
                    j.Property<DateTime?>("DtAdded").ValueGeneratedOnAdd();
                });
        }
    }
}