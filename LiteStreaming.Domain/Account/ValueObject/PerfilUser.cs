using Domain.Account.Agreggates;
using Domain.Core.ValueObject;

namespace Domain.Account.ValueObject;

public record PerfilUser : BasePerfil
{
    private static readonly object lockObj = new object();
    private static readonly ThreadLocal<bool> isConverting = new ThreadLocal<bool>(() => false);

    public static implicit operator UserType(PerfilUser pu)
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

    public static implicit operator PerfilUser(UserType perfil)
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
                return new PerfilUser(perfil);
            }
            finally
            {
                isConverting.Value = false;
            }
        }
    }

    public static implicit operator PerfilUser(int perfil)
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
                return new PerfilUser(perfil);
            }
            finally
            {
                isConverting.Value = false;
            }
        }
    }

    public static bool operator ==(PerfilUser perfilUsuario, UserType perfil) => perfilUsuario?.Id == (int)perfil;
    public static bool operator !=(PerfilUser perfilUsuario, UserType perfil) => !(perfilUsuario?.Id == (int)perfil);

    public virtual IList<User> Users { get; set; }

    public PerfilUser() : base() { }

    public PerfilUser(UserType type) : base(type) { }
}
