using Domain.Account.Agreggates;
using Repository.Interfaces;

namespace Repository.Repositories;
public class CustomerRepository : RepositoryBase<Customer>, IRepository<Customer>
{
    public RegisterContext Context { get; set; }
    public CustomerRepository(RegisterContext context) : base(context)
    {
        Context = context;
    }
}