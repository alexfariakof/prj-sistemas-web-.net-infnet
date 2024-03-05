using Domain.Account.Agreggates;
using Domain.Streaming.Agreggates;

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

        var dsMusic = this.Context.Set<Music>().ToList();
        entity.Musics = entity.Musics.Select(music => dsMusic.First(m =>  m.Id == music.Id)).ToList();
        this.Context.Add(entity);
        this.Context.SaveChanges();
    }

    public override void Update(PlaylistPersonal entity)
    {
        var entityToUpdate = this.Context.PlaylistPersonal.Where(p => p.Id.Equals(entity.Id)).First();
        var dsMusic = this.Context.Set<Music>().ToList();
        entityToUpdate.Musics = entity.Musics.Select(music => dsMusic.First(m => m.Id == music.Id)).ToList();
        this.Context.Update(entityToUpdate);
        this.Context.SaveChanges();
    }
}