using Domain.Account.ValueObject;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Application.Conta.Dto;
public class CustomerDto
{    
    public Guid Id { get; set; }

    [Required]
    public string? Name { get; set; } = null;

    [Required]
    [EmailAddress]
    public string? Email { get; set; } = null;

    [Required]
    [PasswordPropertyText]
    public string? Password { get; set; } = null;

    [Required]
    public string? CPF { get; set; } = null;

    [Required]
    public DateTime Birth { get; set; }

    [Required]
    public Phone? Phone { get; set; } = null;

    [Required]
    public Address? Address { get; set; } = null;

    public Guid FlatId { get; set; }

    [Required]
    public CardDto? Card { get; set; } = null;
}