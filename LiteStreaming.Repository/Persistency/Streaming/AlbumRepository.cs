using Domain.Streaming.Agreggates;
using Repository.Abstractions;
using Repository.Interfaces;

namespace Repository.Persistency.Streaming;
public class AlbumRepository : BaseRepository<Album>, IRepository<Album>
{
    public RegisterContext Context { get; }
    public AlbumRepository(RegisterContext context) : base(context)
    {
        Context = context;
    }
}