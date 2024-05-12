using Domain.Core.Aggreggates;
using Domain.Core.ValueObject;

namespace Domain.Admin.Agreggates;
public class AdminAccount : BaseAccount
{
    public string? Name { get; set; }
    public override Login Login { get; set; }
    public override DateTime DtCreated { get; set; } = DateTime.Now;
    public override Perfil PerfilType { get; set; }    
}