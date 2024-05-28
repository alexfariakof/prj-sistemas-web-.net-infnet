namespace LiteStreaming.STS.Model;

internal class User
{
    internal enum UserTypeValues
    {
        Invalid = 0,
        Admin = 1,
        Normal = 2,
        Customer = 3,
        Merchant = 4
    }

    public Guid Id { get; set; }
    public string? Email {  get; set; }
    public string? Password {  get; set; }
    public int PerfilTypeId { get; set; }
    public UserTypeValues UserType { get => (UserTypeValues)this.PerfilTypeId; }
}
