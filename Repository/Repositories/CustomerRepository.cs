using Domain.Account.Agreggates;
using Domain.Transactions.ValueObject;

namespace Repository.Repositories;
public class CustomerRepository : RepositoryBase<Customer>, IRepository<Customer>
{
    public RegisterContext Context { get; set; }
    public CustomerRepository(RegisterContext context) : base(context)
    {
        Context = context;
    }
    public override void Save(Customer entity)
    {
        var dsCreditCardBrand = this.Context.Set<CreditCardBrand>();
        foreach (var card in entity.Cards) 
            card.CardBrand = dsCreditCardBrand.Where(c => card.CardBrand != null &&  c.Id == (int)card.CardBrand.CardBrand).FirstOrDefault();

        this.Context.Add(entity);       
        this.Context.SaveChanges();
    }
}