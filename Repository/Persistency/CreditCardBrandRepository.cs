using Domain.Transactions.ValueObject;
using Repository.Interfaces;

namespace Repository.Persistency;
public class CreditCardBrandRepository : ICreditCardBrandRepository
{
    private RegisterContext Context { get; set; }
    public CreditCardBrandRepository(RegisterContext context)
    {
        Context = context;
    }

    public CreditCardBrand GetById(int id)
    {
        return this.Context.Set<CreditCardBrand>().Find(id) ?? new();
    }
}