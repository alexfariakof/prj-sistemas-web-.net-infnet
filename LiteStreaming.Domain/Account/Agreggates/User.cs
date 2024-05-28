using Domain.Account.ValueObject;
using Domain.Core.Aggreggates;

namespace Domain.Account.Agreggates;
public class User : BaseAccount
{
    public virtual PerfilUser PerfilType { get; set; }
}