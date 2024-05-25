using Domain.Streaming.Agreggates;
using Repository.Abastractions;
using Repository.Interfaces;

namespace Repository.Persistency.Streaming;
public class MusicRepository : BaseRepository<Music>, IRepository<Music>
{
    public RegisterContext Context { get; set; }
    public MusicRepository(RegisterContext context) : base(context)
    {
        Context = context;
    }
}