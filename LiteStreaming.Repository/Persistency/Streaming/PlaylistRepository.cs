using Domain.Streaming.Agreggates;
using Repository.Abastractions;
using Repository.Interfaces;

namespace Repository.Persistency.Streaming;
public class PlaylistRepository : BaseRepository<Playlist>, IRepository<Playlist>
{
    private new RegisterContext Context { get; set; }
    public PlaylistRepository(RegisterContext context) : base(context)
    {
        Context = context;
    }
}