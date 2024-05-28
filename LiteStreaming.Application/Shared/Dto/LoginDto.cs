using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Application.Shared.Dto;
public class LoginDto
{
    [Required(ErrorMessage = "O campo email é obrigatório!")]
    [EmailAddress(ErrorMessage = "O campo email é inválido!")]
    public string? Email { get; set; }

    [Required(ErrorMessage = "O campo senha é obrigatório!")]
    [PasswordPropertyText]
    public string? Password { get; set; }
}
