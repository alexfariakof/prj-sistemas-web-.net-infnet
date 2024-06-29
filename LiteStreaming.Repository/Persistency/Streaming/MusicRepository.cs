using Domain.Streaming.Agreggates;
using LiteStreaming.Repository.Abstractions.Interfaces;
using Repository.Abstractions;

namespace Repository.Persistency.Streaming;
public class MusicRepository : BaseRepository<Music>, IRepository<Music>
{
    public RegisterContext Context { get; }
    public MusicRepository(RegisterContext context) : base(context)
    {
        Context = context;
    }
}