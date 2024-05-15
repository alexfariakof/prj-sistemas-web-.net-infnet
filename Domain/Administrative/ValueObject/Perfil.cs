using Domain.Administrative.Agreggates;
using Domain.Core.ValueObject;

namespace Domain.Administrative.ValueObject;
public record Perfil : BasePerfil
{
    public static implicit operator UserType(Perfil pu) => (UserType)pu.Id;
    public static implicit operator Perfil(UserType perfil) => new Perfil(perfil);
    public static implicit operator Perfil(int perfil) => new Perfil(perfil);
    public static bool operator ==(Perfil perfilUsuario, UserType perfil) => perfilUsuario?.Id == (int)perfil;
    public static bool operator !=(Perfil perfilUsuario, UserType perfil) => !(perfilUsuario?.Id == (int)perfil);

    public virtual IList<AdministrativeAccount> Users { get; set; }
    
    public Perfil() : base() { }

    public Perfil(UserType type) : base(type) { }
}