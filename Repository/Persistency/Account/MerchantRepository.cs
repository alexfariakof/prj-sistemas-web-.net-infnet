using Domain.Account.Agreggates;
using Domain.Account.ValueObject;
using Repository.Abastractions;
using Repository.Interfaces;

namespace Repository.Persistency.Account;
public class MerchantRepository : BaseRepository<Merchant>, IRepository<Merchant>
{
    public RegisterContext Context { get; set; }
    public MerchantRepository(RegisterContext context) : base(context)
    {
        Context = context;
    }

    public override void Save(Merchant entity)
    {
        entity.User.PerfilType = Context.Set<PerfilUser>().Find(entity.User.PerfilType.Id);
        Context.Add(entity);
        Context.SaveChanges();
    }

    public override void Update(Merchant entity)
    {
        entity.User.PerfilType = Context.Set<PerfilUser>().Find(entity.User.PerfilType.Id);
        Context.Update(entity);
        Context.SaveChanges();
    }
}