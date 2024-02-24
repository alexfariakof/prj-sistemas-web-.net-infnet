using Domain.Streaming.Agreggates;
using __mock__;

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

        var login = MockLogin.GetFaker();

        // Act
        customer.CreateAccount(new Agreggates.Customer { Name = "John Doe", Birth = DateTime.Now, CPF = "123456789" }, login, flat, card);

        // Assert
        Assert.Equal("John Doe", customer.Name);
        Assert.Equal(login, customer.Login) ;
        Assert.Equal("123456789", customer.CPF);
        Assert.Equal(DateTime.Now.Date, customer.Birth.Date);
        Assert.Single(customer.Cards, card);
        Assert.Single(customer.Playlists);
    }
}