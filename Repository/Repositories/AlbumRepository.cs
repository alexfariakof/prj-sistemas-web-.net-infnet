using Domain.Streaming.Agreggates;
using Repository.Interfaces;

namespace Repository.Repositories;
public class AlbumRepository : RepositoryBase<Album>, IRepository<Album>
{
    public RegisterContext Context { get; set; }
    public AlbumRepository(RegisterContext context) : base(context)
    {
        Context = context;
    }   
}