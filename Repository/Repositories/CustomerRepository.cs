using Domain.Account.Agreggates;

namespace Repository.Repository;
public class CustomerRepository : RepositoryBase<Customer>, IRepository<Customer>
{
    public RegisterContext Context { get; set; }
    public CustomerRepository(RegisterContext context) : base(context)
    {
        Context = context;
    }

    /*
    public Usuario GetById(Guid id)
    {
        return this.Context.Usuarios
                   .Include(x => x.Assinaturas) //Caso não esteja usando lazy loading
                   .Include(x => x.Playlists)
                   .Include(x => x.Notificacoes)
                   //.AsSplitQuery() //Quebra a consulta por cada tipo
                   .FirstOrDefault(x => x.Id == id);
    }
    */
}