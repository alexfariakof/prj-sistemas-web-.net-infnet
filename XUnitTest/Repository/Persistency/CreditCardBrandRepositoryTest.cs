using Domain.Transactions.ValueObject;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace Repository.Persistency;
public class CreditCardBrandRepositoryTest
{
    private Mock<RegisterContext> contextMock;

    public CreditCardBrandRepositoryTest()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<RegisterContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase_CreditCardBrandRepositoryTest")
            .Options;

        contextMock = new Mock<RegisterContext>(options);
    }

    [Fact]
    public void GetById_Should_Return_CreditCardBrand_With_Correct_Id()
    {
        // Arrange
        var mockRepoCreditiCardBrand = Usings.MockDataSetCreditCardBrand();
        contextMock.Object.Add(mockRepoCreditiCardBrand);
        
        var repository = new CreditCardBrandRepository(contextMock.Object);        
        
        var mockCardBrand = new CreditCardBrand((int)CardBrand.Visa, CardBrand.Visa.ToString());
        contextMock.Setup(c => c.Set<CreditCardBrand>().Find(It.IsAny<int>())).Returns(mockCardBrand);
        mockRepoCreditiCardBrand.Setup(x => x.GetById(It.IsAny<int>())).Returns(mockCardBrand);

        // Act
        var result = repository.GetById(mockCardBrand.Id);

        // Assert
        Assert.Equal(mockCardBrand, result);
    }
}