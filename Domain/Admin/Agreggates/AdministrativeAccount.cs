using Domain.Admin.ValueObject;
using Domain.Core.Aggreggates;

namespace Domain.Admin.Agreggates;
public class AdministrativeAccount : BaseAccount
{
    public string? Name { get; set; }
    public virtual Perfil PerfilType { get; set; }
}