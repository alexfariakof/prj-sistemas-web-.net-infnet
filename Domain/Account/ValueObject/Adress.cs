using Domain.Account.Agreggates;
using Domain.Core.Aggreggates;

namespace Domain.Account.ValueObject;
public class Adress : BaseModel
{
    public Adress() { }
    public string? Zipcode { get; set; }
    public string? Street { get; set; }
    public string? Number { get; set; }
    public string? Neighborhood { get; set; }
    public string? City { get; set; }
    public string? State { get; set; }
    public string? Complement { get; set; }
    public string? Country { get; set; }
}
