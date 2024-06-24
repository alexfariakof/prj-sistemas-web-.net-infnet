using Domain.Administrative.Agreggates;
using Domain.Core.ValueObject;

namespace Domain.Administrative.ValueObject;

public record Perfil : BasePerfil
{
    private static readonly object lockObj = new object();
    private static readonly ThreadLocal<bool> isConverting = new ThreadLocal<bool>(() => false);

    public static implicit operator UserType(Perfil pu)
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

    public static implicit operator Perfil(UserType perfil)
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
                return new Perfil(perfil);
            }
            finally
            {
                isConverting.Value = false;
            }
        }
    }

    public static implicit operator Perfil(int perfil)
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
                return new Perfil(perfil);
            }
            finally
            {
                isConverting.Value = false;
            }
        }
    }

    public static bool operator ==(Perfil perfilUsuario, UserType perfil) => perfilUsuario?.Id == (int)perfil;
    public static bool operator !=(Perfil perfilUsuario, UserType perfil) => !(perfilUsuario?.Id == (int)perfil);

    public virtual IList<AdministrativeAccount> Users { get; set; }

    public Perfil() : base() { }

    public Perfil(UserType type) : base(type) { }
}
