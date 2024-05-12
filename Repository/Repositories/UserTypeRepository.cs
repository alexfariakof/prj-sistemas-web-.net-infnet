using Domain.Core.ValueObject;
using Repository.Interfaces;

namespace Repository.Repositories;
public class UserTypeRepository : IUserTypeRepository
{
    private RegisterContext Context { get; set; }
    public UserTypeRepository(RegisterContext context)
    {
        Context = context;
    }
    public Perfil GetById(int id)
    {
        return this.Context.Set<Perfil>().Find(id) ?? new();
    }
}