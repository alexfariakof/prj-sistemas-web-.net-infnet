namespace Domain.Core.ValueObject;
public abstract record BasePerfil
{
    public static implicit operator UserType(BasePerfil pu) => (UserType)pu.Id;
    public static implicit operator BasePerfil(UserType perfil) => perfil;
    public static implicit operator BasePerfil(int perfil) => perfil;

    public static bool operator ==(BasePerfil perfilUsuario, UserType perfil) => perfilUsuario?.Id == (int)perfil;
    public static bool operator !=(BasePerfil perfilUsuario, UserType perfil) => !(perfilUsuario?.Id == (int)perfil);

    public enum UserType
    {
        Invalid = 0,
        Admin = 1,
        Normal = 2,
        Customer = 3,
        Merchant = 4
    }

    public virtual int Id { get; set; }
    public virtual string Description { get; set; }

    public BasePerfil() 
    { 
        Id = 0;
        Description = GetDescription(0);
    }

    public BasePerfil(UserType type)
    {
        Id = (int)type;
        Description = GetDescription(type);
    }

    private string GetDescription(UserType userType = UserType.Invalid)
    {
        if (UserType.Admin == (UserType)userType)
        {
            return "Administrador";
        }
        else if (UserType.Normal == (UserType)userType)
        {
            return "Normal";
        }
        else if (UserType.Customer == userType)
        {
            return "Customer";
        }
        else if (UserType.Merchant == userType)
        {
            return "Merchant";
        }
        else if (UserType.Invalid == userType)
        {
            return "Invalid";
        }

        throw new ArgumentException("Tipo de usuário inexistente.");
    }
}