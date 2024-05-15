using Domain.Administrative.ValueObject;
using Repository.Interfaces.Administrative;

namespace Repository.Persistency.Administrative;
public class PerfilRepository : IPerfilRepository
{
    private RegisterContextAdministravtive Context { get; set; }
    public PerfilRepository(RegisterContextAdministravtive context)
    {
        Context = context;
    }
    public Perfil GetById(int id)
    {
        return this.Context.Set<Perfil>().Find(id) ?? new();
    }
}