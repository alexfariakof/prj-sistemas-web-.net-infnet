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
        entity.Customer = this.Context.Set<Customer>().FirstOrDefault(c => c.User.Id.Equals(entity.CustomerId));
        var loadedMusics = this.Context.Set<Music>().Where(m => entity.Musics.Select(em => em.Id).Contains(m.Id)).ToList();
        entity.Musics = loadedMusics;
        this.Context.Add(entity);
        this.Context.SaveChanges();
    }

    /// <summary>
    /// Atualiza uma entidade PlaylistPersonal no contexto.
    /// </summary>
    /// <param name="entity">A entidade PlaylistPersonal a ser atualizada.</param>
    /// <remarks>
    /// Utilizar LINQ diretamente em um contexto sem convertê-lo em um objeto concreto torna
    /// a unidade de trabalho menos testável ou intestável devido à sua dependência direta da infraestrutura do banco de dados.
    /// Recomenda-se o uso de métodos ou extensões específicas para consultas LINQ no contexto
    /// para melhorar a modularidade, desacoplamento e a testabilidade do código.
    /// </remarks>
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