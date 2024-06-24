using Domain.Account.Agreggates;
using Domain.Core.ValueObject;

namespace Domain.Account.ValueObject;
public record PerfilUser : BasePerfil
{
    public virtual IList<User>? Users { get; set; }

    public PerfilUser() : base() { }

    public PerfilUser(UserType type) : base(type) { }

    public static implicit operator UserType(PerfilUser pu)
    {
        return (UserType)pu.Id;
    }

    public static implicit operator PerfilUser(UserType perfil)
    {
        return new PerfilUser(perfil);
    }
     

    public static bool operator ==(PerfilUser perfilUsuario, UserType perfil)
    {
        return perfilUsuario?.Id == (int)perfil;
    }

    public static bool operator !=(PerfilUser perfilUsuario, UserType perfil)
    {
        return !(perfilUsuario?.Id == (int)perfil);
    }
}
