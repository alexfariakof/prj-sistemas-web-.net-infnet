using Domain.Streaming.Agreggates;
using Repository.Abastractions;
using Repository.Interfaces;

namespace Repository.Persistency.Streaming;
public class PlaylistRepository : BaseRepository<Playlist>, IRepository<Playlist>
{
    public RegisterContext Context { get; }
    public PlaylistRepository(RegisterContext context) : base(context)
    {
        Context = context;
    }
}