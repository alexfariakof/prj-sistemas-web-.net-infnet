using Domain.Streaming.ValueObject;

namespace Domain.Streaming;
public class DurationTest
{
    [Theory]
    [InlineData(150, "02:30")]
    [InlineData(65, "01:05")]
    [InlineData(3600, "60:00")]
    public void Should_Return_Correct_Format_Formatted_ptBr(int durationValue, string expectedFormattedValue)
    {
        // Arrange
        var duration = new Duration(durationValue);

        // Act
        var formattedValue = duration.Formatted_ptBr();

        // Assert
        Assert.Equal(expectedFormattedValue, formattedValue);
    }

    [Fact]
    public void Should_Throw_Exception_For_Negative_Value_In_Constructor()
    {
        // Arrange, Act & Assert
        Assert.Throws<ArgumentException>(() => new Duration(-5));
    }

    [Fact]
    public void Should_Convert_Duration_To_Int_Implicit_Operator()
    {
        // Arrange
        var duration = new Duration(120);

        // Act
        int value = duration;

        // Assert
        Assert.Equal(120, value);
    }

    [Fact]
    public void Should_Convert_Int_To_Duration_Implicit_Operator()
    {
        // Arrange
        int value = 180;

        // Act
        Duration duration = value;

        // Assert
        Assert.Equal(value, duration.Value);
    }
}