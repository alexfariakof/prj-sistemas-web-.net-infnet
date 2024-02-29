using Application.Transactions.Dto;
using Domain.Account.ValueObject;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Application.Account.Dto;
public class MerchantDto
{    
    public Guid Id { get; set; }

    [Required]
    public string? Name { get; set; }

    [Required]
    [EmailAddress]
    public string? Email { get; set; }

    [Required]
    [PasswordPropertyText]
    public string? Password { get; set; }

    [Required]
    public string? CPF { get; set; }

    [Required]
    public string? CNPJ { get; set; }

    [Required]
    public Phone? Phone { get; set; }

    [Required]
    public AddressDto? Address { get; set; }

    public Guid FlatId { get; set; }

    [Required]
    public CardDto? Card { get; set; }
}