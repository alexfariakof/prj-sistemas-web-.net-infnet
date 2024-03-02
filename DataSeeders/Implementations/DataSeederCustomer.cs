using Domain.Account.Agreggates;
using Domain.Account.ValueObject;
using Domain.Streaming.Agreggates;
using Domain.Transactions.Agreggates;
using Domain.Transactions.ValueObject;
using Repository;

namespace DataSeeders.Implementations
{
    public class DataSeederCustomer : IDataSeeder
    {
        private readonly RegisterContext _context;

        public DataSeederCustomer(RegisterContext context)
        {
            _context = context;
        }

        public void SeedData()
        {
            var customer = new Customer
            {
                Name = "User Test",
                Birth = new DateTime(1990, 1, 1),
                CPF = "123.456.789-01",
                Phone = new Phone { Number = "+5521993879312" },
                Login = new Login { Email = "user@test.com", Password = "12345" }
            };

            var address = new Address()
            {
                Zipcode = "12345-678",
                Street = "Main Street",
                Number = "123",
                Neighborhood = "Downtown",
                City = "Cityville",
                State = "ST",
                Complement = "Apt 456",
                Country = "Countryland"
            };
            customer.AddAdress(address);

            var card = new Card()
            {
                Number = "5564 7434 7840 3985",
                Validate = new ExpiryDate(new DateTime(2099, 1, 1)),
                CVV = "388",
                CardBrand = CreditCardBrand.IdentifyCard("5564 7434 7840 3985"),
                Active = true
            };
            customer.AddCard(card);
            

            try
            {
                _context.Add(customer);
                _context.SaveChanges();
            }
            catch
            {
                _context.Remove(customer);
                Console.WriteLine($"Dados já cadastrados na base de dados");
            }
        }
    }
}