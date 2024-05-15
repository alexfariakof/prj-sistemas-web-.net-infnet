using Domain.Core.ValueObject;

namespace Domain.Core.Aggreggates;
public abstract class BaseAccount : Base
{
    public virtual Login Login { get; set; }
    public virtual DateTime DtCreated { get; set; } = DateTime.Now;    
}
