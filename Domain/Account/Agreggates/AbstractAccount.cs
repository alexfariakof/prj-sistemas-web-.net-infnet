using Domain.Account.ValueObject;
using Domain.Core.Aggreggates;
using Domain.Core.ValueObject;
using Domain.Notifications;
using Domain.Streaming.Agreggates;
using Domain.Transactions.Agreggates;
using Domain.Transactions.ValueObject;

namespace Domain.Account.Agreggates;
public abstract class AbstractAccount<T> : BaseModel
{
    public string? Name { get; set; }
    public string? CPF { get; set; } = String.Empty;
    public Login? Login { get; set; }
    public virtual IList<Adress> Addresses { get; set; } = new List<Adress>();
    public virtual IList<Card> Cards { get; set; } = new List<Card>();    
    public virtual IList<Signature> Signatures { get; set; } = new List<Signature>();
    public virtual IList<Notification> Notifications { get; set; } = new List<Notification>();
    public abstract void CreateAccount(T obj, Adress adress, Flat flat, Card card);    
    public void AddAdress(Adress address) => this.Addresses.Add(address);
    public void AddCard(Card card) => this.Cards.Add(card);
    public void AddFlat(Customer customer, Flat flat, Card card)
    {
        IsValidCreditCard(card.Number ?? "");
        card.Id = Guid.NewGuid();
        card.CardBrand = CreditCardBrand.IdentifyCard(card.Number ?? "");
        card.CreateTransaction(customer, new Monetary(flat.Value), flat.Description ?? "");
        DisableActiveSigniture();
        this.Signatures.Add(new Signature()
        {
            Active = true,
            Flat = flat,
            DtActivation = DateTime.Now,
        });
    }
    private void DisableActiveSigniture()
    {
        if (this.Signatures.Count > 0 && this.Signatures.Any(x => x.Active))
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            this.Signatures.FirstOrDefault(x => x.Active).Active = false ;
#pragma warning restore CS8602 // Dereference of a possibly null reference.
    }

    private static void IsValidCreditCard(string creditCardNumber)
    {
        var cardInfo = CreditCardBrand.IdentifyCard(creditCardNumber);

        if (!cardInfo.IsValid)
            throw new ArgumentException($"Cartão { cardInfo.Name } inválido.");
        else if (cardInfo.CardBrand == CardBrand.Invalid)
            throw new ArgumentException($"Cartão { cardInfo.Name }.");                

    }
}
