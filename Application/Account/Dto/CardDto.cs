using Domain.Transactions.ValueObject;

namespace Application.Conta.Dto;
public class CardDto
{
    public Guid Id { get; set; }
    public bool Active { get; set; }
    public decimal Limit { get; set; }
    public string? Number { get; set; }
    public ExpiryDate? Validate { get; set; }
    public string? CVV { get; set; }

}