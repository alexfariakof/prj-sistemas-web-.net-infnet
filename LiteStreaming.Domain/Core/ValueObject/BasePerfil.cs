using Domain.Account.ValueObject;

namespace Domain.Core.ValueObject;

public abstract record BasePerfil
{
    private static readonly object lockObj = new object();
    private static readonly ThreadLocal<bool> isConverting = new ThreadLocal<bool>(() => false);

    public static implicit operator UserType(BasePerfil pu)
    {
        lock (lockObj)
        {
            if (isConverting.Value)
            {
                throw new InvalidOperationException("Recursion detected in conversion.");
            }

            isConverting.Value = true;
            try
            {
                return (UserType)pu.Id;
            }
            finally
            {
                isConverting.Value = false;
            }
        }
    }

    public static implicit operator BasePerfil(UserType perfil)
    {
        lock (lockObj)
        {
            if (isConverting.Value)
            {
                throw new InvalidOperationException("Recursion detected in conversion.");
            }

            isConverting.Value = true;
            try
            {
                return CreatePerfil(perfil);
            }
            finally
            {
                isConverting.Value = false;
            }
        }
    }

    public static implicit operator BasePerfil(int perfil)
    {
        lock (lockObj)
        {
            if (isConverting.Value)
            {
                throw new InvalidOperationException("Recursion detected in conversion.");
            }

            isConverting.Value = true;
            try
            {
                return CreatePerfil((UserType)perfil);
            }
            finally
            {
                isConverting.Value = false;
            }
        }
    }

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
        Description = GetDescription(UserType.Invalid);
    }

    public BasePerfil(UserType type)
    {
        Id = (int)type;
        Description = GetDescription(type);
    }

    private string GetDescription(UserType userType)
    {
        return userType switch
        {
            UserType.Admin => "Administrador",
            UserType.Normal => "Normal",
            UserType.Customer => "Customer",
            UserType.Merchant => "Merchant",
            UserType.Invalid => "Invalid",
            _ => throw new ArgumentException("Tipo de usuário inexistente.")
        };
    }

    private static BasePerfil CreatePerfil(UserType userType)
    {
        return new PerfilUser(userType);
    }
}
