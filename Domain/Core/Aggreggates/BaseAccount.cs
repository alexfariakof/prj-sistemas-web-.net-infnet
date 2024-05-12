using Domain.Core.ValueObject;

namespace Domain.Core.Aggreggates;
public abstract class BaseAccount : Base
{
    public abstract Login Login { get; set; }
    public abstract DateTime DtCreated { get; set; }
    public abstract Perfil PerfilType { get; set; }    
}
