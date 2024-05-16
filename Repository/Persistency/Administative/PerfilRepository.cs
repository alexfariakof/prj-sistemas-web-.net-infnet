using Domain.Administrative.ValueObject;
using Repository.Abastractions;
using Repository.Interfaces;

namespace Repository.Persistency.Administrative;
public class PerfilRepository : BaseRepository<Perfil>, IRepository<Perfil>
{
    private RegisterContextAdministravtive Context { get; set; }
    public PerfilRepository(RegisterContextAdministravtive context) : base(context)
    {
        Context = context;
    }
    public override Perfil GetById(int id)
    {
        return this.Context.Set<Perfil>().Find(id) ?? new();
    }
}