using Domain.Streaming.Agreggates;
using LiteStreaming.Repository.Abstractions.Interfaces;
using Repository.Abstractions;

namespace Repository.Persistency.Streaming;
public class PlaylistRepository : BaseRepository<Playlist>, IRepository<Playlist>
{
    public RegisterContext Context { get; }
    public PlaylistRepository(RegisterContext context) : base(context)
    {
        Context = context;
    }
}