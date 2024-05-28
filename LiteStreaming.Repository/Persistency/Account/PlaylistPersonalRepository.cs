using Domain.Account.Agreggates;
using Repository.Abastractions;
using Repository.Interfaces;

namespace Repository.Persistency.Account;
public class PlaylistPersonalRepository : BaseRepository<PlaylistPersonal>, IRepository<PlaylistPersonal>
{
    private new RegisterContext Context { get; set; }
    public PlaylistPersonalRepository(RegisterContext context) : base(context)
    {
        Context = context;
    }
}