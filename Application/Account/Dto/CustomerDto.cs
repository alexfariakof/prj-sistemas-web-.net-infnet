using Domain.Account.ValueObject;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Application.Conta.Dto;
public class CustomerDto
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
    public DateTime Birth { get; set; }

    [Required]
    public Phone? Phone { get; set; }

    [Required]
    public Adress? Address { get; set; }

    public Guid FlatId { get; set; }

    [Required]
    public CardDto? Card { get; set; }
}