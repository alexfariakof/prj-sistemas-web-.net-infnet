using Domain.Administrative.ValueObject;
using Repository.Abastractions;
using Repository.Interfaces;

namespace Repository.Persistency.Administrative;
public class PerfilRepository : BaseRepository<Perfil>, IRepository<Perfil>
{
    public RegisterContextAdministravtive Context { get;  }
    public PerfilRepository(RegisterContextAdministravtive context) : base(context)
    {
        Context = context;
    }
    public override Perfil GetById(int id)
    {
        return Context.Set<Perfil>().Find(id) ?? new();
    }
}