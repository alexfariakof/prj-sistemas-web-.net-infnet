using Domain.Admin.Agreggates;

namespace Domain.Admin.ValueObject;

public record Perfil
{
    public static implicit operator PerfilType(Perfil pu) => (PerfilType)pu.Id;
    public static implicit operator Perfil(PerfilType perfil) => new Perfil(perfil);
    public static implicit operator Perfil(int perfil) => new Perfil(perfil);
    public static bool operator ==(Perfil perfilUsuario, PerfilType perfil) => perfilUsuario?.Id == (int)perfil;
    public static bool operator !=(Perfil perfilUsuario, PerfilType perfil) => !(perfilUsuario?.Id == (int)perfil);

    public enum PerfilType
    {
        Admin = 1,
        Normal = 2,
        Invalid = 0,
    }

    public int Id { get; set; }
    public string Description { get; set; }
    public virtual IList<AdministrativeAccount> Users { get; set; }
    public Perfil() { }
    public Perfil(PerfilType type)
    {
        Id = (int)type;
        Description = SetDescription(type);
    }

    private string SetDescription(PerfilType userType = PerfilType.Invalid)
    {
        if (PerfilType.Admin == userType)
        {
            return "Administrador";
        }
        else if (PerfilType.Normal == userType)
        {
            return "Normal";
        }

        return userType.ToString();
    }
}