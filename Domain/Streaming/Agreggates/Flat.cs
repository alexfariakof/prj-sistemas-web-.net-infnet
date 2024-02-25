using Domain.Core.Aggreggates;
using Domain.Core.ValueObject;

namespace Domain.Streaming.Agreggates;
public class Flat : BaseModel
{
    public String Name { get; set; }
    public String Description { get; set; }
    public Monetary Value { get; set; }
}
