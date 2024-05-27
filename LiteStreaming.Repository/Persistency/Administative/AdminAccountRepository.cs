using Domain.Administrative.Agreggates;
using Domain.Administrative.ValueObject;
using Repository.Abastractions;
using Repository.Interfaces;

namespace Repository.Persistency;
public class AdminAccountRepository : BaseRepository<AdministrativeAccount>, IRepository<AdministrativeAccount>
{
    private new RegisterContextAdministravtive Context { get; set; }
    public AdminAccountRepository(RegisterContextAdministravtive context) : base(context)
    {
        Context = context;
    }

    public override void Save(AdministrativeAccount entity)
    {
        entity.PerfilType = this.Context.Set<Perfil>().Find(entity.PerfilType.Id);
        Context.Add(entity);
        Context.SaveChanges();
    }

    public override void Update(AdministrativeAccount entity)
    {
        entity.PerfilType = this.Context.Set<Perfil>().Find(entity.PerfilType.Id);
        Context.Update(entity);
        Context.SaveChanges();
    }
}