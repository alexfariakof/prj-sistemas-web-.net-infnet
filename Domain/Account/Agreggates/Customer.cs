using Domain.Account.ValueObject;
using Domain.Streaming.Agreggates;
using Domain.Transactions.Agreggates;

namespace Domain.Account.Agreggates
{
    public class Customer : AbstractAccount<Customer>
    {
        private const string PLAYLIST_NAME = "Favoritas";
        public string CPF { get; set; }
        public DateTime Birth { get; set; }
        public Phone Phone { get; set; } = new Phone();
        public Address Address { get; set; } = new Address();
        public List<PlaylistPersonal> Playlists { get; set; } = new List<PlaylistPersonal>();
        public List<Transaction> Transactions { get; set; } = new List<Transaction>();
        public override void CreateAccount(Customer customer, Login login, Flat flat, Card card)
        {
            Name = customer.Name;            
            Birth = customer.Birth;
            CPF = customer.CPF;
            Login = login;
            AddFlat(flat, card);
            AddCard(card);
            CreatePlaylist(name: PLAYLIST_NAME, @public: false);
        }
        public void CreatePlaylist(string name, bool @public = true)
        {
            this.Playlists.Add(new PlaylistPersonal()
            {
                Name = name,
                IsPublic = @public,
                DtCreated = DateTime.Now,
                Customer = this
            });
        }        
    }
}
