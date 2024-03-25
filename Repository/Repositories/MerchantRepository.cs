using Domain.Account.Agreggates;
using Repository.Interfaces;

namespace Repository.Repositories;
public class MerchantRepository : RepositoryBase<Merchant>, IRepository<Merchant>
{
    public RegisterContext Context { get; set; }
    public MerchantRepository(RegisterContext context) : base(context)
    {
        Context = context;
    }    
}