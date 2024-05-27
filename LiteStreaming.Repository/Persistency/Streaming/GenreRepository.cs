using Domain.Streaming.Agreggates;
using Repository.Abastractions;
using Repository.Interfaces;

namespace Repository.Persistency.Streaming;
public class GenreRepository : BaseRepository<Genre>, IRepository<Genre>
{
    private new  RegisterContext Context { get; set; }
    public GenreRepository(RegisterContext context) : base(context)
    {
        Context = context;
    }
}