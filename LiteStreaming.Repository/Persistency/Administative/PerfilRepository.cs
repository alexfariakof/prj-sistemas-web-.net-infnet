using Domain.Administrative.ValueObject;
using LiteStreaming.Repository.Abstractions.Interfaces;
using Repository.Abstractions;

namespace Repository.Persistency.Administrative;
public class PerfilRepository : BaseRepository<Perfil>, IRepository<Perfil>
{
    public RegisterContextAdministrative Context { get;  }
    public PerfilRepository(RegisterContextAdministrative context) : base(context)
    {
        Context = context;
    }
    public override Perfil GetById(int id)
    {
        return Context.Set<Perfil>().Find(id) ?? new();
    }
}