using Domain.Streaming.Agreggates;
using Repository.Interfaces;

namespace Repository.Repositories;
public class BandRepository : RepositoryBase<Band>, IRepository<Band>
{
    public RegisterContext Context { get; set; }
    public BandRepository(RegisterContext context) : base(context)
    {
        Context = context;
    }   
}