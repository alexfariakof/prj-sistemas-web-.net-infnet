using Domain.Account.Agreggates;
using Repository.Abastractions;
using Repository.Interfaces;

namespace Repository.Persistency;
public class UserRepository : BaseRepository<User>, IRepository<User>
{
    public RegisterContext Context { get; set; }
    public UserRepository(RegisterContext context) : base(context)
    {
        Context = context;
    }
}