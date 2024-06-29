using Domain.Account.Agreggates;
using LiteStreaming.Repository.Abstractions.Interfaces;
using Repository.Abstractions;

namespace Repository.Persistency.Account;
public class PlaylistPersonalRepository : BaseRepository<PlaylistPersonal>, IRepository<PlaylistPersonal>
{
    public RegisterContext Context { get; }
    public PlaylistPersonalRepository(RegisterContext context) : base(context)
    {
        Context = context;
    }
}