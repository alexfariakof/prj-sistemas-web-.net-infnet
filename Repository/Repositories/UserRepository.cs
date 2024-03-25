using Domain.Account.Agreggates;
using Repository.Interfaces;

namespace Repository.Repositories;
public class UserRepository : RepositoryBase<User>, IRepository<User>
{
    public RegisterContext Context { get; set; }
    public UserRepository(RegisterContext context) : base(context)
    {
        Context = context;
    }
}