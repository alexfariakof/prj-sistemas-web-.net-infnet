using Domain.Account.ValueObject;
using Domain.Streaming.Agreggates;
using Domain.Transactions.Agreggates;

namespace Domain.Account.Agreggates
{
    public class Merchant : AbstractAccount<Merchant>
    {
        public string CNPJ { get; set; }
        public Phone Phone { get; set; } = new Phone();
        public Address Address { get; set; } = new Address();
        public List<Transaction> Transactions { get; set; }
        public override void CreateAccount(Merchant merchant, Login login,  Flat flat, Card card)
        {
            Name = merchant.Name;
            CNPJ = merchant.CNPJ;
            Login = login;
            AddFlat(flat, card);
            AddCard(card);
        }
    }
}
