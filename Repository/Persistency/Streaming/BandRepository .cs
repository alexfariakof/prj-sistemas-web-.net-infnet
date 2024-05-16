using Domain.Streaming.Agreggates;
using Repository.Abastractions;
using Repository.Interfaces;

namespace Repository.Persistency.Streaming;
public class BandRepository : BaseRepository<Band>, IRepository<Band>
{
    public RegisterContext Context { get; set; }
    public BandRepository(RegisterContext context) : base(context)
    {
        Context = context;
    }
}