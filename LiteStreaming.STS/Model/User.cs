namespace LiteStreaming.STS.Model;

internal class User
{
    public Guid Id { get; set; }
    public string Email {  get; set; }
    public string Password {  get; set; }
    public int UserType { get; set; }
}
