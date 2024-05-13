using Domain.Core.ValueObject;

namespace Domain.Core;
public class MonetaryTest
{
    [Theory]
    [InlineData(50.0, "R$ 50,00")]
    [InlineData(100.5, "R$ 100,50")]
    [InlineData(1234.56, "R$ 1.234,56")]
    public void Should_Return_Correct_Format_Formatted_ptBr(decimal value, string expectedFormattedValue)
    {
        // Arrange
        var monetary = new Monetary(value);

        // Act
        var formattedValue = monetary.FormattedPtBr();

        // Assert
        Assert.Equal(expectedFormattedValue, formattedValue);
    }

    [Fact]
    public void Should_Not_Allow_Negative_Value_Monetary()
    {
        // Arrange, Act, Assert
        Assert.Throws<ArgumentException>(() => new Monetary(-10.0m));
    }

    [Theory]
    [InlineData(50.0, 5000)]
    [InlineData(100.5, 10050)]
    [InlineData(1234.56, 123456)]
    public void GetCents_Should_Return_Correct_Value(decimal value, int expectedCents)
    {
        // Arrange
        var monetary = new Monetary(value);

        // Act
        var cents = monetary.GetCents();

        // Assert
        Assert.Equal(expectedCents, cents);
    }

    [Fact]
    public void Implicit_Conversion_Should_Work_Correctly()
    {
        // Arrange
        decimal originalValue = 75.25m;

        // Act
        Monetary monetary = originalValue;
        decimal convertedValue = monetary;

        // Assert
        Assert.Equal(originalValue, convertedValue);
    }

    [Fact]
    public void Operator_Equal_and_NotEqual_Should_Work_Correctly()
    {
        // Arrange
        Monetary monetary1 = new Monetary(50.0m);
        Monetary monetary2 = new Monetary(100.0m);
        Monetary monetary3 = new Monetary(50.0m);

        // Act & Assert
        Assert.True(monetary1 != monetary2);
        Assert.True(monetary2 != monetary3);
        Assert.True(monetary1 == monetary3);
    }
}