using Domain.Account.Agreggates;
using Domain.Streaming.Agreggates;
using Repository.Interfaces;

namespace Repository.Repositories;
public class PlaylistPersonalRepository : RepositoryBase<PlaylistPersonal>, IRepository<PlaylistPersonal>
{
    public RegisterContext Context { get; set; }
    public PlaylistPersonalRepository(RegisterContext context) : base(context)
    {
        Context = context;
    }

    public override void Save(PlaylistPersonal entity)
    {
        entity.Customer = this.Context.Set<Customer>().FirstOrDefault(c => c.User.Id.Equals(entity.CustomerId));
        var loadedMusics = this.Context.Set<Music>().Where(m => entity.Musics.Select(em => em.Id).Contains(m.Id)).ToList();
        entity.Musics = loadedMusics;
        this.Context.Add(entity);
        this.Context.SaveChanges();
    }

    public override void Update(PlaylistPersonal entity)
    {
        var entityToUpdate = this.Context.Set<PlaylistPersonal>().FirstOrDefault(p => p.Id.Equals(entity.Id));
        entityToUpdate.Customer = this.Context.Set<Customer>().FirstOrDefault(c => c.User.Id.Equals(entity.CustomerId));
        var loadedMusics = this.Context.Set<Music>().Where(m => entity.Musics.Select(em => em.Id).Contains(m.Id)).ToList();
        entityToUpdate.Musics = loadedMusics;

        this.Context.Update(entityToUpdate);
        this.Context.SaveChanges();
    }
}