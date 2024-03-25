using Domain.Streaming.Agreggates;
using Repository.Interfaces;

namespace Repository.Repositories;
public class PlaylistRepository : RepositoryBase<Playlist>, IRepository<Playlist>
{
    public RegisterContext Context { get; set; }
    public PlaylistRepository(RegisterContext context) : base(context)
    {
        Context = context;
    }   
}