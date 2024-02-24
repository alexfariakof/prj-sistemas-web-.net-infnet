using __mock__;
using Domain.Account.Agreggates;
using Domain.Streaming.Agreggates;
using Domain.Transactions.Agreggates;
using Moq;

namespace Domain.Account;
public class AbstractAccountTests
{
    [Fact]
    public void Should_AddCard_To_Card_List()
    {
        // Arrange
        var account = new Mock<AbstractAccount<Customer>>().Object;
        var card = MockCard.GetFaker();

        // Act
        account.AddCard(card);

        // Assert
        Assert.Contains(card, account.Cards);
    }

    [Fact]
    public void Should_Create_Transaction_And_AddSignature_When_Add_Flat()
    {
        // Arrange
        var accountMock = new Mock<AbstractAccount<Customer>>();
        accountMock.VerifyAll();

        var account = accountMock.Object;
        account.Id = Guid.NewGuid();
        account.Name = " Account Test ";
        account.Login = MockLogin.GetFaker();

        var flat = new Flat
        {
            Id = Guid.NewGuid(),
            Name = "Test Flat",
            Value = 50.0m,
            Description = "Test Description"
        };
        var cardMock = new Mock<Card>();
        cardMock.VerifyAll();
        var card = cardMock.Object;

        var mockCard = MockCard.GetFaker();
        card.Id = mockCard.Id;
        card.Number = mockCard.Number;
        card.Validate = mockCard.Validate;
        card.CVV = mockCard.CVV;
        card.Active = true;
        card.Limit = mockCard.Limit;

        // Act
        account.Cards.Add(card);
        account.AddFlat(flat, card);

        // Assert
        Mock.Verify(accountMock, cardMock);
        Assert.Single(account.Signatures, s => s.Flat == flat && s.Active);
        Assert.Empty(account.Notifications);
    }

    [Fact]
    public void Should_Disable_Active_Signature_DisableActiveSigniture()
    {
        // Arrange
        var accountMock = new Mock<AbstractAccount<Customer>>();
        var account = accountMock.Object;
        var activeSignature = new Signature { Active = true };
        var inactiveSignature = new Signature { Active = false };
        account.Signatures.Add(activeSignature);
        account.Signatures.Add(inactiveSignature);
        var flat = new Flat
        {
            Id = Guid.NewGuid(),
            Name = "Test Flat",
            Value = 50.0m,
            Description = "Test Description"
        };
        var cardMock = new Mock<Card>();
        cardMock.VerifyAll();
        var card = cardMock.Object;

        var mockCard = MockCard.GetFaker();
        card.Id = mockCard.Id;
        card.Number = mockCard.Number;
        card.Validate = mockCard.Validate;
        card.CVV = mockCard.CVV;
        card.Active = true;
        card.Limit = mockCard.Limit;

        // Act
        account.AddFlat(flat, card);

        // Assert
        Mock.Verify(accountMock, cardMock);
        Assert.Single(account.Signatures, s => s.Flat == flat && s.Active);
        Assert.False(activeSignature.Active);
        Assert.False(inactiveSignature.Active);
        Assert.Empty(account.Notifications);
    }
}