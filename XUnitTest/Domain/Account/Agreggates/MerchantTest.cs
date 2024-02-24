using Domain.Streaming.Agreggates;
using __mock__;

namespace Domain.Account;
public class MerchantTest
{
    [Fact]
    public void Should_Create_Account_With_Flat_Card_And_Playlist()
    {
        // Arrange
        var merchantMock = MockMerchant.GetFaker();
        var merchant = merchantMock;
        var flat = new Flat
        {
            Id = Guid.NewGuid(),
            Name = "Test Flat",
            Value = 50.0m,
            Description = "Test Description"
            
        };

        var card = MockCard.GetFaker();
        card.Active = true;

        var login = MockLogin.GetFaker();

        // Act
        merchant.CreateAccount(new Agreggates.Merchant { Name = "John Doe", CNPJ = "123456789" }, login, flat, card);

        // Assert
        Assert.Equal("John Doe", merchant.Name);
        Assert.Equal(login, merchant.Login) ;
        Assert.Equal("123456789", merchant.CNPJ);
        Assert.Single(merchant.Cards, card);
    }
}