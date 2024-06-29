using Domain.Streaming.Agreggates;
using LiteStreaming.Repository.Abstractions.Interfaces;
using Repository.Abstractions;

namespace Repository.Persistency.Streaming;
public class FlatRepository : BaseRepository<Flat>, IRepository<Flat>
{
    public RegisterContext Context { get; }
    public FlatRepository(RegisterContext context) : base(context)
    {
        Context = context;
    }
}