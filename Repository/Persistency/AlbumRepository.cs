using Domain.Streaming.Agreggates;
using Repository.Abastractions;
using Repository.Interfaces;

namespace Repository.Persistency;
public class AlbumRepository : BaseRepository<Album>, IRepository<Album>
{
    public RegisterContext Context { get; set; }
    public AlbumRepository(RegisterContext context) : base(context)
    {
        Context = context;
    }   
}