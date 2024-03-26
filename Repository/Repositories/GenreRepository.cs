using Domain.Streaming.Agreggates;
using Repository.Interfaces;

namespace Repository.Repositories;
public class GenreRepository : RepositoryBase<Genre>, IRepository<Genre>
{
    public RegisterContext Context { get; set; }
    public GenreRepository(RegisterContext context) : base(context)
    {
        Context = context;
    }   
}