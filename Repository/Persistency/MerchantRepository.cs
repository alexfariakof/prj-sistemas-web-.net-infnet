using Domain.Account.Agreggates;
using Repository.Abastractions;
using Repository.Interfaces;

namespace Repository.Persistency;
public class MerchantRepository : BaseRepository<Merchant>, IRepository<Merchant>
{
    public RegisterContext Context { get; set; }
    public MerchantRepository(RegisterContext context) : base(context)
    {
        Context = context;
    }

    public override void Save(Merchant entity)
    {
        entity.User.PerfilType = this.Context.PerfilUser.Find(entity.User.PerfilType.Id);
        Context.Add(entity);
        Context.SaveChanges();
    }

    public override void Update(Merchant entity)
    {
        entity.User.PerfilType = this.Context.PerfilUser.Find(entity.User.PerfilType.Id);
        Context.Update(entity);
        Context.SaveChanges();
    }
}