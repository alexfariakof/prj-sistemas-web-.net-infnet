using Domain.Account.Agreggates;
using Domain.Account.ValueObject;
using Domain.Transactions.Agreggates;
using Domain.Transactions.ValueObject;
using Repository;

namespace DataSeeders.Implementations;
public class DataSeederMerchant : IDataSeeder
{
    private readonly RegisterContext _context;
    public DataSeederMerchant(RegisterContext context)
    {
        _context = context;
    }
    public void SeedData()
    {
        if (!_context.Merchant.Any())
        {
            var merchant = new Merchant()
            {
                Name = "Company Test",
                CNPJ = "12.345.678/0001-90",
                Customer = new()
                {
                    Name = "Owner Company",
                    Birth = new DateTime(1983, 1, 1),
                    CPF = "123.456.789-01",
                    Phone = "+5521992879319",
                    Login = new()
                    {
                        Email = "company@test.com",
                        Password = "12345"
                    }
                },
                Addresses = new[] {
                    new Address()
                    {
                        Zipcode = "12345-678",
                        Street = "Main Street",
                        Number = "123",
                        Neighborhood = "Downtown",
                        City = "Cityville",
                        State = "ST",
                        Complement = "Apt 456",
                        Country = "Countryland"
                    }
                },
                Cards = new[]
                {
                    new Card()
                    {
                        Number = "5564 7434 7840 3985",
                        Validate = new ExpiryDate(new DateTime(2099, 1, 1)),
                        CVV = "388",
                        CardBrand = CreditCardBrand.IdentifyCard("5564 7434 7840 3985"),
                        Active = true
                    }
                } 
            };

            try
            {
                _context.Add(merchant);
                _context.SaveChanges();
            }
            catch
            {
                _context.Add(merchant);
                Console.WriteLine($"Dados já cadastrados na base de dados");
            }
        }
    }
}