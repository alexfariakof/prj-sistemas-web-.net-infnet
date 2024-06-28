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

    private ExpiryDate((int Month, int Year) values) : this(new DateTime(values.Year, values.Month, 1, 0, 0, 0, DateTimeKind.Local))
    {
        Month = values.Month;
        Year = values.Year;
    }

    public string Formatted_ptBr()
    {
        return $"{Value.Month:D2}/{Value.Year % 100:D2}";
    }
}