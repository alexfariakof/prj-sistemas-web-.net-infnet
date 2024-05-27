using System.Globalization;

namespace Domain.Core.ValueObject;
public record Monetary
{
    public decimal Value { get; set; }
    public static implicit operator decimal(Monetary d) => d.Value;
    public static implicit operator Monetary(decimal value) => new Monetary(value);
    public static bool operator ==(Monetary monetary, decimal value) => monetary?.Value == value;
    public static bool operator !=(Monetary monetary, decimal value) => !(monetary == value);

    public Monetary() { }
    public Monetary(decimal value)
    {
        if (value < 0)
            throw new ArgumentException("Valor monetário não pode ser negativo");

        Value = value;
    }

    public int GetCents()
    {
        return (int)(Value*100);
    }
    public string FormattedPtBr()
    {
        return $"R$ {Value.ToString("N2", new CultureInfo("pt-BR"))}";
    }
}
