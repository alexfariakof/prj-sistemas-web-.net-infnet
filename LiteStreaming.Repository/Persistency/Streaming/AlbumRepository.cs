using Domain.Streaming.Agreggates;
using LiteStreaming.Repository.Abstractions.Interfaces;
using Repository.Abstractions;

namespace Repository.Persistency.Streaming;
public class AlbumRepository : BaseRepository<Album>, IRepository<Album>
{
    public RegisterContext Context { get; }
    public AlbumRepository(RegisterContext context) : base(context)
    {
        Context = context;
    }
}