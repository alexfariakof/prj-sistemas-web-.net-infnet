using Domain.Transactions.ValueObject;
using Repository.Abastractions;
using Repository.Interfaces;

namespace Repository.Persistency.Transactions;
public class CreditCardBrandRepository : BaseRepository<CreditCardBrand>, IRepository<CreditCardBrand>
{
    private RegisterContext Context { get; set; }
    public CreditCardBrandRepository(RegisterContext context) : base(context)
    {
        Context = context;
    }

    public override CreditCardBrand GetById(int id)
    {
        return Context.Set<CreditCardBrand>().Find(id) ?? new();
    }
}