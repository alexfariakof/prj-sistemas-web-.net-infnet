using Domain.Account.Agreggates;
using Domain.Transactions.ValueObject;

namespace Repository.Repositories;

#pragma warning disable CS8602 // Dereference of a possibly null reference.
#pragma warning disable CS0108 // Member hides inherited member; missing new keyword
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
            card.CardBrand = dsCreditCardBrand.Where(c => c.Id == (int)card.CardBrand.CardBrand).FirstOrDefault();

        this.Context.Add(entity);       
        this.Context.SaveChanges();
    }
}