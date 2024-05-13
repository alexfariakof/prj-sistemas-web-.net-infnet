using Domain.Account.Agreggates;
using Repository.Abastractions;
using Repository.Interfaces;

namespace Repository.Persistency;
public class CustomerRepository : BaseRepository<Customer>, IRepository<Customer>
{
    public RegisterContext Context { get; set; }
    public CustomerRepository(RegisterContext context) : base(context)
    {
        Context = context;
    }
}