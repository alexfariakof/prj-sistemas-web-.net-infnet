using Domain.Account.ValueObject;
using Repository.Interfaces;

namespace Repository.Repositories;
public class UserTypeRepository : IUserTypeRepository
{
    private RegisterContext Context { get; set; }
    public UserTypeRepository(RegisterContext context)
    {
        Context = context;
    }
    public PerfilUser GetById(int id)
    {
        return this.Context.Set<PerfilUser>().Find(id) ?? new();
    }
}