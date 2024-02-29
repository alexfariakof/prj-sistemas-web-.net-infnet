using Domain.Streaming.Agreggates;

namespace Domain.Account;
public class CustomerTest
{
    [Fact]
    public void Should_Create_Account_With_Flat_Card_And_Playlist()
    {
        // Arrange
        var customerMock = MockCustomer.GetFaker();
        var customer = customerMock;
        var flat = new Flat
        {
            Id = Guid.NewGuid(),
            Name = "Test Flat",
            Value = 50.0m,
            Description = "Test Description"
        };

        var card = MockCard.GetFaker();
        card.Active = true;

        // Act
        customer.CreateAccount(customerMock, MockAddress.GetFaker(), flat, card);

        // Assert
        Assert.Equal(customerMock.Name, customer.Name);
        Assert.Equal(customerMock.Login, customer.Login) ;
        Assert.Equal(customerMock.CPF, customer.CPF);
        Assert.Equal(customerMock.Birth, customer.Birth);
        Assert.Single(customer.Cards, card);
        Assert.Single(customer.Playlists);
    }
}