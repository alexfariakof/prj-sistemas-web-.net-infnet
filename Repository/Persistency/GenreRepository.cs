using Domain.Streaming.Agreggates;
using Repository.Abastractions;
using Repository.Interfaces;

namespace Repository.Persistency;
public class GenreRepository : BaseRepository<Genre>, IRepository<Genre>
{
    public RegisterContext Context { get; set; }
    public GenreRepository(RegisterContext context) : base(context)
    {
        Context = context;
    }   
}