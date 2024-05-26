using System.Text.Json.Serialization;

namespace Domain.Core.Aggreggates;
public abstract class BaseDto
{
    public virtual Guid Id { get; set; }

    [JsonIgnore]
    public virtual Guid UsuarioId { get; set; }
}
