using Domain.Transactions.ValueObject;

namespace Domain.Transactions;
public class ExpiryDateTest
{
    [Fact]
    public void Should_Create_Instance_With_Valid_Value()
    {
        // Arrange
        var validDate = new DateTime(2023, 12, 31);

        // Act
        var expiryDate = new ExpiryDate(validDate);

        // Assert
        Assert.Equal(validDate, expiryDate.Value);
        Assert.Equal(validDate.Month, expiryDate.Month);
        Assert.Equal(validDate.Year, expiryDate.Year);
    }

    [Fact]
    public void Should_Return_Correct_Formatted_ptBr()
    {
        // Arrange
        var date = new DateTime(2023, 12, 1);
        var expiryDate = new ExpiryDate(date);

        // Act
        var formattedPtBr = expiryDate.Formatted_ptBr();

        // Assert
        Assert.Equal("12/23", formattedPtBr);
    }
}
