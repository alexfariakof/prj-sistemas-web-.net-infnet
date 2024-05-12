using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Domain.Account.Agreggates;
using Domain.Streaming.Agreggates;

namespace Repository.Mapping.Account;
public class PlaylistPersonalMap : IEntityTypeConfiguration<PlaylistPersonal>
{
    public void Configure(EntityTypeBuilder<PlaylistPersonal> builder)
    {
        builder.ToTable(nameof(PlaylistPersonal));

        builder.HasKey(playlist => playlist.Id);
        builder.Property(playlist => playlist.Id).ValueGeneratedOnAdd();
        builder.Property(playlist => playlist.Name).IsRequired().HasMaxLength(50);
        builder.Property(playlist => playlist.CustomerId).IsRequired();
        builder.Property(playlist => playlist.IsPublic).IsRequired();
        builder.Property(playlist => playlist.DtCreated).ValueGeneratedOnAdd();

        builder.HasMany(playlist => playlist.Musics)
        .WithMany(music => music.PersonalPlaylists)
        .UsingEntity<Dictionary<string, object>>(
            "MusicPlayListPersonal",
            j => j
            .HasOne<Music>()
            .WithMany()
            .HasForeignKey("MusicId"),
        j => j
            .HasOne<PlaylistPersonal>()
            .WithMany()
            .HasForeignKey("PlaylistPersonalId"),
        j =>
        {
            j.HasKey("MusicId", "PlaylistPersonalId");
            j.Property<DateTime>("DtAdded").ValueGeneratedOnAdd();
        });
    }
}