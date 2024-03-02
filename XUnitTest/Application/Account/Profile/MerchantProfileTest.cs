using AutoMapper;
using Application.Account.Profile;
using Application.Account.Dto;
using Domain.Account.Agreggates;
using Application.Transactions.Dto;

namespace Application.Account;
public class MerchantProfileTest
{

    [Fact]
    public void Map_MerchantDto_To_Merchant_IsValid()
    {
        // Arrange
        var mapper = new Mapper(new MapperConfiguration(cfg => {
            cfg.AddProfile<MerchantProfile>();
        }));

        var merchantDto = new MerchantDto
        {
            Id = Guid.NewGuid(),
            Name = "John Doe",
            Email = "john.doe@example.com",
            Password = "password123",
            CPF = "12345678901",
            CNPJ = "1425478547584616465",
            Phone = "123456789",
            Address = new AddressDto
            {
                Zipcode = "12345-678",
                Street = "Main Street",
                Number = "123",
                Neighborhood = "Downtown",
                City = "Cityville",
                State = "ST",
                Complement = "Apt 456",
                Country = "Countryland"
            },
            FlatId = Guid.NewGuid(),
            Card = new CardDto
            {
                Limit = 1000,
                Number = "1234567812345678",
                Validate = DateTime.Now.AddYears(1),
                CVV = "123"
            }
        };

        // Act
        var merchant = mapper.Map<Merchant>(merchantDto);

        // Assert
        Assert.NotNull(merchant);
        Assert.Equal(merchantDto.Id, merchant.Id);
        Assert.Equal(merchantDto.Name, merchant.Name);
        Assert.Equal(merchantDto.Email, merchant.Customer.Login.Email);
        Assert.Equal(merchantDto.CPF, merchant.Customer.CPF);
        Assert.Equal(merchantDto.CNPJ, merchant.CNPJ);
        Assert.NotNull(merchant.Customer.Phone);
        Assert.Equal(merchantDto.Phone, merchant.Customer.Phone?.Number);
        Assert.NotNull(merchant.Addresses);
        Assert.NotNull(merchant.Cards);
    }

    [Fact]
    public void Map_Merchant_To_MerchantDto_IsValid()
    {
        // Arrange
        var mapper = new Mapper(new MapperConfiguration(cfg => {
            cfg.AddProfile<MerchantProfile>();
        }));

        var merchant = MockMerchant.GetFaker();
        merchant.AddAdress(MockAddress.GetFaker());        
        merchant.AddCard(MockCard.GetFaker());

        var customer = new Customer()
        {
            Name = merchant.Name,
            Addresses = merchant.Addresses,
            Cards = merchant.Cards,
            Login = merchant.Customer.Login,
            CPF = merchant.Customer.CPF,
            Phone = merchant.Customer.Phone,
            Signatures = merchant.Signatures,
        };
        merchant.AddFlat(customer, MockFlat.GetFaker(), merchant.Cards.FirstOrDefault());

        // Act
        var merchantDto = mapper.Map<MerchantDto>(merchant);

        // Assert
        Assert.NotNull(merchantDto);
        Assert.Equal(merchant.Id, merchantDto.Id);
        Assert.Equal(merchant.Name, merchantDto.Name);
        Assert.Equal(merchant.Customer.Login.Email, merchantDto.Email);
        Assert.Equal(merchant.Customer.CPF, merchantDto.CPF);
        Assert.Equal(merchant.CNPJ, merchantDto.CNPJ);
        Assert.NotNull(merchantDto.Phone);
        Assert.Equal(merchant.Customer.Phone?.Number, merchantDto.Phone);
        Assert.NotNull(merchantDto.Address);
        Assert.Equal(merchant.Addresses.First().Zipcode, merchantDto.Address.Zipcode);
        Assert.Equal(merchant.Addresses.First().Street, merchantDto.Address.Street);
        Assert.Equal(merchant.Addresses.First().Number, merchantDto.Address.Number);
        Assert.Equal(merchant.Addresses.First().Neighborhood, merchantDto.Address.Neighborhood);
        Assert.Equal(merchant.Addresses.First().City, merchantDto.Address.City);
        Assert.Equal(merchant.Addresses.First().State, merchantDto.Address.State);
        Assert.Equal(merchant.Addresses.First().Complement, merchantDto.Address.Complement);
        Assert.Equal(merchant.Addresses.First().Country, merchantDto.Address.Country);
        Assert.Null(merchantDto.Card);
    }
}
