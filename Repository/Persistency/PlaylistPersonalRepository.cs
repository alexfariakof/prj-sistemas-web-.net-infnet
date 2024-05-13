using Domain.Account.Agreggates;
using Repository.Abastractions;
using Repository.Interfaces;

namespace Repository.Persistency;
public class PlaylistPersonalRepository : BaseRepository<PlaylistPersonal>, IRepository<PlaylistPersonal>
{
    public RegisterContext Context { get; set; }
    public PlaylistPersonalRepository(RegisterContext context) : base(context)
    {
        Context = context;
    }
}