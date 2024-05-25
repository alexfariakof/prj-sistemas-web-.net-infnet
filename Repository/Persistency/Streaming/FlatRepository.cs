using Domain.Streaming.Agreggates;
using Repository.Abastractions;
using Repository.Interfaces;

namespace Repository.Persistency.Streaming;
public class FlatRepository : BaseRepository<Flat>, IRepository<Flat>
{
    public RegisterContext Context { get; set; }
    public FlatRepository(RegisterContext context) : base(context)
    {
        Context = context;
    }
}