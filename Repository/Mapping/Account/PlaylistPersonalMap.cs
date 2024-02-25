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

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedOnAdd();
        builder.Property(x => x.Name).IsRequired().HasMaxLength(50);
        builder.Property(x => x.IsPublic).IsRequired();
        builder.Property(x => x.DtCreated).IsRequired();

        builder.HasMany(x => x.Musics)
        .WithMany(x => x.PersonalPlaylists)
        .UsingEntity<Dictionary<string, object>>(
            "MusicPlayList",
            j => j
            .HasOne<Music>()
            .WithMany()
            .HasForeignKey("MusicId"),
        j => j
            .HasOne<PlaylistPersonal>()
            .WithMany()
            .HasForeignKey("PlaylistId"),
        j =>
        {
            j.HasKey("MusicId", "PlaylistId");
            j.Property<DateTime>("DtAdded");
        });
    }
}