using Domain.Account.Agreggates;
using Repository.Abastractions;
using Repository.Interfaces;

namespace Repository.Persistency;
public class MerchantRepository : BaseRepository<Merchant>, IRepository<Merchant>
{
    public RegisterContext Context { get; set; }
    public MerchantRepository(RegisterContext context) : base(context)
    {
        Context = context;
    }    
}