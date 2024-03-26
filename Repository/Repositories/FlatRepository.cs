using Domain.Streaming.Agreggates;
using Repository.Interfaces;

namespace Repository.Repositories;
public class FlatRepository : RepositoryBase<Flat>, IRepository<Flat>
{
    public RegisterContext Context { get; set; }
    public FlatRepository(RegisterContext context) : base(context)
    {
        Context = context;
    }   
}