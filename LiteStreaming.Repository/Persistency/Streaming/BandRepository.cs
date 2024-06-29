using Domain.Streaming.Agreggates;
using LiteStreaming.Repository.Abstractions.Interfaces;
using Repository.Abstractions;

namespace Repository.Persistency.Streaming;
public class BandRepository : BaseRepository<Band>, IRepository<Band>
{
    public RegisterContext Context { get; }
    public BandRepository(RegisterContext context) : base(context)
    {
        Context = context;
    }
}