using Domain.Account.ValueObject;
using Repository.Abastractions;
using Repository.Interfaces;

namespace Repository.Persistency.Account;
public class UserTypeRepository : BaseRepository<PerfilUser>, IRepository<PerfilUser>
{
    private new RegisterContext Context { get; set; }
    public UserTypeRepository(RegisterContext context) : base(context)
    {
        Context = context;
    }
    public override PerfilUser GetById(int id)
    {
        return Context.Set<PerfilUser>().Find(id) ?? new();
    }
}