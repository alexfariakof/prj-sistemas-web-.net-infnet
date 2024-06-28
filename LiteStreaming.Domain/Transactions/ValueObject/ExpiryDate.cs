using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Transactions.ValueObject;
public record ExpiryDate
{
    public static implicit operator (int Month, int Year)(ExpiryDate expiryDate) => (expiryDate.Month, expiryDate.Year);
    public static implicit operator ExpiryDate((int Month, int Year) values) => new ExpiryDate(values);

    public DateTime Value { get; set; }

    [NotMapped]
    public int Month { get; init; }

    [NotMapped]
    public int Year { get; init; }

    public ExpiryDate(DateTime value)
    {
        Value = value;
        Month = value.Month;
        Year = value.Year;
    }

    public ExpiryDate((int Month, int Year) values)
    {
        Month = values.Month;
        Year = values.Year;
        Value = new DateTime(Year, Month, 1);
    }

    public string Formatted_ptBr()
    {
        return $"{Value.Month:D2}/{Value.Year % 100:D2}";
    }
}
