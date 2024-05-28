using Domain.Administrative.Agreggates;
using Domain.Administrative.ValueObject;
using Repository.Abastractions;
using Repository.Interfaces;

namespace Repository.Persistency;
public class AdminAccountRepository : BaseRepository<AdministrativeAccount>, IRepository<AdministrativeAccount>
{
    public RegisterContextAdministravtive Context { get;  }
    public AdminAccountRepository(RegisterContextAdministravtive context) : base(context)
    {
        Context = context;
    }

    public override void Save(AdministrativeAccount entity)
    {
        entity.PerfilType = Context.Set<Perfil>().Find(entity.PerfilType.Id) ?? throw new ArgumentNullException();
        Context.Add(entity);
        Context.SaveChanges();
    }

    public override void Update(AdministrativeAccount entity)
    {
        entity.PerfilType = Context.Set<Perfil>().Find(entity.PerfilType.Id) ?? throw new ArgumentNullException();
        Context.Update(entity);
        Context.SaveChanges();
    }
}