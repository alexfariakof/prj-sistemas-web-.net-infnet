using Domain.Streaming.Agreggates;
using Repository.Abstractions;
using Repository.Interfaces;

namespace Repository.Persistency.Streaming;
public class FlatRepository : BaseRepository<Flat>, IRepository<Flat>
{
    public RegisterContext Context { get; }
    public FlatRepository(RegisterContext context) : base(context)
    {
        Context = context;
    }
}