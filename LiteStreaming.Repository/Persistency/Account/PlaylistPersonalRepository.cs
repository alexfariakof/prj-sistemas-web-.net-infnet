using Domain.Account.Agreggates;
using Repository.Abstractions;
using Repository.Interfaces;

namespace Repository.Persistency.Account;
public class PlaylistPersonalRepository : BaseRepository<PlaylistPersonal>, IRepository<PlaylistPersonal>
{
    public RegisterContext Context { get; }
    public PlaylistPersonalRepository(RegisterContext context) : base(context)
    {
        Context = context;
    }
}