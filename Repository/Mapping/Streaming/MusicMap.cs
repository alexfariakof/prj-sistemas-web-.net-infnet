using Domain.Streaming.Agreggates;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.Mapping.Streaming;
public class MusicMap : MusicEntityMap<Playlist>
{
    protected override void ConfigureCustom(EntityTypeBuilder<Music<Playlist>> builder)
    {

        builder.HasMany(x => x.Playlists).WithMany(m => m.Musics);
    }
}