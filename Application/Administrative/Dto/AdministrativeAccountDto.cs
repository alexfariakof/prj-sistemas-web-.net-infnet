using Application.Transactions.Dto;
using Domain.Administrative.ValueObject;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Application.Administrative.Dto;
public class AdministrativeAccountDto
{
    [JsonIgnore]
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
    public PerfilDto? PerfilType { get; set; }

}
