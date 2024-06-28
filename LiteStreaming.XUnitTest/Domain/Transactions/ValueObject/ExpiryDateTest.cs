using Domain.Transactions.ValueObject;
using System.Globalization;

namespace Domain.Transactions;
public class ExpiryDateTests
{
    [Fact]
    public void ImplicitConversionToTuple_ShouldReturnCorrectValues()
    {
        // Arrange
        var expiryDate = new ExpiryDate(new DateTime(2024, 6, 27));

        // Act
        (int Month, int Year) result = expiryDate;

        // Assert
        Assert.Equal(6, result.Month);
        Assert.Equal(2024, result.Year);
    }

    [Fact]
    public void ImplicitConversionFromTuple_ShouldCreateCorrectExpiryDate()
    {
        // Arrange
        var values = (Month: 6, Year: 2024);

        // Act
        ExpiryDate expiryDate = values;

        // Assert
        Assert.Equal(6, expiryDate.Month);
        Assert.Equal(2024, expiryDate.Year);
        Assert.Equal(new DateTime(2024, 6, 1), expiryDate.Value);
    }

    [Fact]
    public async Task Formatted_ptBr_ShouldReturnCorrectFormat()
    {
        // Arrange
        var expiryDate = new ExpiryDate(new DateTime(2024, 6, 27));
        var originalCulture = CultureInfo.CurrentCulture;

        CultureInfo.CurrentCulture = new CultureInfo("pt-BR");

        try
        {
            // Act
            var formatted = await Task.Run(() => expiryDate.Formatted_ptBr());

            // Assert
            Assert.Equal("06/24", formatted);
        }
        finally
        {
            CultureInfo.CurrentCulture = originalCulture;
        }
    }

    [Fact]
    public void ConstructorWithDateTime_ShouldInitializeCorrectly()
    {
        // Arrange
        var date = new DateTime(2024, 6, 27);

        // Act
        var expiryDate = new ExpiryDate(date);

        // Assert
        Assert.Equal(6, expiryDate.Month);
        Assert.Equal(2024, expiryDate.Year);
        Assert.Equal(date, expiryDate.Value);
    }
}
