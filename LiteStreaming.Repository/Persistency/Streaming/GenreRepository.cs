using Domain.Streaming.Agreggates;
using LiteStreaming.Repository.Abstractions.Interfaces;
using Repository.Abstractions;

namespace Repository.Persistency.Streaming;
public class GenreRepository : BaseRepository<Genre>, IRepository<Genre>
{
    public RegisterContext Context { get; }
    public GenreRepository(RegisterContext context) : base(context)
    {
        Context = context;
    }
}