using Domain.Account.Agreggates;

namespace Domain.Core.ValueObject;

public record Perfil
{

    public static implicit operator PerfilType(Perfil pu) => (PerfilType)pu.Id;
    public static implicit operator Perfil(PerfilType perfil) => new Perfil(perfil);
    public static implicit operator Perfil(int perfil) => new Perfil(perfil);
    public static bool operator ==(Perfil perfilUsuario, PerfilType perfil) => perfilUsuario?.Id == (int)perfil;
    public static bool operator !=(Perfil perfilUsuario, PerfilType perfil) => !(perfilUsuario == perfil);

    public enum PerfilType
    {
        Admin = 1,        
        Customer = 2,
        Merchant = 3,
        Invalid = 99,
    }

    public int Id { get; set; }
    public string Description { get; set; }
    public virtual IList<User> Users { get; set; }
    public Perfil() { }
    public Perfil(PerfilType type)
    {
        Id = (int)type;
        Description = SetDescription(type);
    }

    private string SetDescription(PerfilType userType = PerfilType.Invalid)
    {
        if (PerfilType.Customer == userType)
        {
            return "Customer";
        }
        else if (PerfilType.Merchant == userType)
        {
            return "Merchant";
        }
        else if (PerfilType.Admin == userType)
        {
            return "Admin";
        }

        return userType.ToString();
    }
}