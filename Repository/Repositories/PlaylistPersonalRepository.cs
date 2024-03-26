using Domain.Account.Agreggates;
using Repository.Interfaces;

namespace Repository.Repositories;
public class PlaylistPersonalRepository : RepositoryBase<PlaylistPersonal>, IRepository<PlaylistPersonal>
{
    public RegisterContext Context { get; set; }
    public PlaylistPersonalRepository(RegisterContext context) : base(context)
    {
        Context = context;
    }
}