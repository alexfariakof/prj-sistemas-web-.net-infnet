using Domain.Streaming.Agreggates;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Repository.Mapping.Streaming;

namespace Domain.Account.Agreggates;

public class MusicPersonalMap : MusicEntityMap<PlaylistPersonal>
{
    protected override void ConfigureCustom(EntityTypeBuilder<Music<PlaylistPersonal>> builder)
    {
        builder.HasMany(x => x.Playlists).WithMany(m => m.Musics);
    }
}