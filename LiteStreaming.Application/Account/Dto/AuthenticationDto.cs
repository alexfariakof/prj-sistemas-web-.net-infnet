namespace Application.Streaming.Dto;
public class AuthenticationDto
{
    public string? access_token { get; set; }
    public bool Authenticated { get; set; }
    public string? UserType { get; set; }
}