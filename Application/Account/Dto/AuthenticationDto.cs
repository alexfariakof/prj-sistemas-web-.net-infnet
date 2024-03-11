namespace Application.Account.Dto;
public class AuthenticationDto
{
    public string AccessToken { get; set; }
    public bool Authenticated { get; set; }
}