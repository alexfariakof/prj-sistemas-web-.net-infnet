using Domain.Core.Aggreggates;
using Domain.Core.ValueObject;

namespace Domain.Account.Agreggates;
public class User : BaseAccount
{
    public override Login Login { get; set; }
    public override DateTime DtCreated { get; set; } = DateTime.Now;
    public override Perfil PerfilType{ get; set; }    
}