﻿using Domain.Account.Agreggates;
using Domain.Account.ValueObject;
using Domain.Transactions.Agreggates;
using Domain.Transactions.ValueObject;
using Repository;

namespace DataSeeders;
public class DataSeederMerchant : IDataSeeder
{
    private readonly RegisterContext _context;
    public DataSeederMerchant(RegisterContext context) => _context = context;

    public void SeedData()
    {
        var user = new User()
        {
            Login = new()
            {
                Email = "user@merchant.com",
                Password = "12345T!"
            },
            PerfilType = _context.PerfilUser.Where(u => u.Id.Equals((int)PerfilUser.UserType.Merchant)).First()
        };

        var merchant = new Merchant()
        {
            Name = "Company Test",
            CNPJ = "12.345.678/0001-90",
            User = user,
            Customer = new()
            {
                Name = "Owner Company",
                Birth = new DateTime(1983, 1, 1),
                CPF = "123.456.789-00",
                Phone = "+5521992879325",
                User = user,
                Flat = _context.Flat.First(f => f.Name.Contains("Premium"))
            },
            Addresses = [
                    new Address()
                    {
                        Zipcode = "12345-678",
                        Street = "Company Street",
                        Number = "123",
                        Neighborhood = "Company",
                        City = "Companyville",
                        State = "ST",
                        Complement = "Apt 456",
                        Country = "CompanyCountryland"
                    }
                ],
            Cards =
            [
                    new Card()
                    {
                        Number = "3478 932908 50247",
                        Validate = new ExpiryDate(new DateTime(2025, 8, 4)),
                        CVV = "7184",
                        CardBrand = _context.CardBrand.Where(c => c.Id == (int)CreditCardBrand.IdentifyCard("3478 932908 50247").CardBrand).FirstOrDefault(),
                        Active = true,
                        Limit = 5000m
                    }
                ],
        };

        try
        {
            _context.Add(merchant);
            _context.SaveChanges();
        }
        catch
        {
            throw;
        }
    }
}