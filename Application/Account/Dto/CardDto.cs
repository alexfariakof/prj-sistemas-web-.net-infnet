namespace Application.Conta.Dto;
public class CardDto
{
    public Guid Id { get; set; }
    public bool Active { get; set; }
    public decimal Limit { get; set; }
    public string? Number { get; set; } = null;
}