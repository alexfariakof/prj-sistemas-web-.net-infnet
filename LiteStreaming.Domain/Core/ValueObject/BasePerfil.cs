using Domain.Account.ValueObject;

namespace Domain.Core.ValueObject;

public abstract record BasePerfil
{
    public enum UserType
    {
        Invalid = 0,
        Admin = 1,
        Normal = 2,
        Customer = 3,
        Merchant = 4
    }

    public virtual int Id { get; set; }
    public virtual string? Description { get; set; }

    protected BasePerfil() { }

    public BasePerfil(UserType type)
    {
        Id = (int)type;
        Description = GetDescription(type);
    }

    private static string GetDescription(UserType userType)
    {
        return userType switch
        {
            UserType.Admin => "Administrador",
            UserType.Normal => "Normal",
            UserType.Customer => "Customer",
            UserType.Merchant => "Merchant",            
            _ => throw new ArgumentException("Tipo de usuário inválido.")
        };
    }

    public static implicit operator UserType(BasePerfil perfil)
    {
        if (perfil == null)
            return UserType.Invalid;

        return (UserType)perfil.Id;
    }

    public static implicit operator BasePerfil(UserType userType)
    {
        return new PerfilUser(userType);
    }

    public static implicit operator BasePerfil(int perfil)
    {
        return new PerfilUser((UserType)perfil);
    }

    public static bool operator ==(BasePerfil perfilUsuario, UserType perfil)
    {
        return perfilUsuario?.Id == (int)perfil;
    }

    public static bool operator !=(BasePerfil perfilUsuario, UserType perfil)
    {
        return !(perfilUsuario?.Id == (int)perfil);
    }
}