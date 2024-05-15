using Domain.Account.Agreggates;
using Repository.Abastractions;
using Repository.Interfaces;

namespace Repository.Persistency;
public class UserRepository : BaseRepository<User>, IRepository<User>
{
    public RegisterContext Context { get; set; }
    public UserRepository(RegisterContext context) : base(context)
    {
        Context = context;
    }

    public override void Save(User entity)
    {
        entity.PerfilType = this.Context.PerfilUser.Find(entity.PerfilType.Id);
        Context.Add(entity);
        Context.SaveChanges();
    }

    public override void Update(User entity)
    {
        entity.PerfilType = this.Context.PerfilUser.Find(entity.PerfilType.Id);
        Context.Update(entity);
        Context.SaveChanges();
    }
}