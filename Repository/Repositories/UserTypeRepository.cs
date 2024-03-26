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
    public UserType GetById(int id)
    {
        return this.Context.Set<UserType>().Find(id) ?? new();
    }
}