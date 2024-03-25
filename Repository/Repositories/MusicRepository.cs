using Domain.Streaming.Agreggates;
using Repository.Interfaces;

namespace Repository.Repositories;
public class MusicRepository : RepositoryBase<Music>, IRepository<Music>
{
    public RegisterContext Context { get; set; }
    public MusicRepository(RegisterContext context) : base(context)
    {
        Context = context;
    }
}