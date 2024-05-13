using Domain.Account.Agreggates;

namespace Domain.Account.ValueObject;

public record PerfilUser
{
    public static implicit operator UserlType(PerfilUser pu) => (UserlType)pu.Id;
    public static implicit operator PerfilUser(UserlType perfil) => new PerfilUser(perfil);
    public static implicit operator PerfilUser(int perfil) => new PerfilUser(perfil);
    public static bool operator ==(PerfilUser perfilUsuario, UserlType perfil) => perfilUsuario?.Id == (int)perfil;
    public static bool operator !=(PerfilUser perfilUsuario, UserlType perfil) => !(perfilUsuario?.Id == (int)perfil);

    public enum UserlType
    {
        Admin = 1,
        Customer = 2,
        Merchant = 3,
        Invalid = 0,
    }

    public int Id { get; set; }
    public string Description { get; set; }
    public virtual IList<User> Users { get; set; }
    public PerfilUser() { }
    public PerfilUser(UserlType type)
    {
        Id = (int)type;
        Description = SetDescription(type);
    }

    private string SetDescription(UserlType userType = UserlType.Invalid)
    {
        if (UserlType.Customer == userType)
        {
            return "Customer";
        }
        else if (UserlType.Merchant == userType)
        {
            return "Merchant";
        }
        else if (UserlType.Admin == userType)
        {
            return "Admin";
        }

        return userType.ToString();
    }
}