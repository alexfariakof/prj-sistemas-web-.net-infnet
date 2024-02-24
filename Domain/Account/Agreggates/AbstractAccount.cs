using Domain.Account.ValueObject;
using Domain.Core.Aggreggates;
using Domain.Core.ValueObject;
using Domain.Notifications;
using Domain.Streaming.Agreggates;
using Domain.Transactions.Agreggates;
using Domain.Transactions.ValueObject;

namespace Domain.Account.Agreggates
{
    public abstract class AbstractAccount<T> : BaseModel
    {
        public string Name { get; set; }
        public Login Login { get; set; }
        public List<Card> Cards { get; set; } = new List<Card>();
        public List<Signature> Signatures { get; set; } = new List<Signature>();
        public List<Notification> Notifications { get; set; } = new List<Notification>();
        public abstract void CreateAccount(T obj, Login login, Flat flat, Card card);
        public void AddCard(Card card) => this.Cards.Add(card);
        public void AddFlat(Flat flat, Card card)
        {
            IsValidCreditCard(card.Number);
            var customer = new Customer() { Name = flat.Name };
            card.CreateTransaction(customer, new Monetary(flat.Value), flat.Description);
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
                this.Signatures.FirstOrDefault(x => x.Active).Active = false;
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
}
