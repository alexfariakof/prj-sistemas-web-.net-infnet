using Domain.Streaming.Agreggates;

namespace Repository.Repositories;

#pragma warning disable CS0108 // Member hides inherited member; missing new keyword
public class FlatRepository : RepositoryBase<Flat>, IRepository<Flat>
{
    public RegisterContext Context { get; set; }
    public FlatRepository(RegisterContext context) : base(context)
    {
        Context = context;
    }   
}