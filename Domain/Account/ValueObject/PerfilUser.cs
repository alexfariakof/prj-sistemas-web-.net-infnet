using Domain.Account.Agreggates;
using Domain.Core.ValueObject;

namespace Domain.Account.ValueObject;

public record PerfilUser : BasePerfil
{
    public static implicit operator UserType(PerfilUser pu) => (UserType)pu.Id;
    public static implicit operator PerfilUser(UserType perfil) => new PerfilUser(perfil);
    public static implicit operator PerfilUser(int perfil) => new PerfilUser(perfil);
    public static bool operator ==(PerfilUser perfilUsuario, UserType perfil) => perfilUsuario?.Id == (int)perfil;
    public static bool operator !=(PerfilUser perfilUsuario, UserType perfil) => !(perfilUsuario?.Id == (int)perfil);

    public virtual IList<User> Users { get; set; }

    public PerfilUser() : base() {}

    public PerfilUser(UserType type): base(type)  { }
}