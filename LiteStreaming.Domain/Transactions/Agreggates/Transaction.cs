using Domain.Account.Agreggates;
using Domain.Core.Aggreggates;
using Domain.Core.ValueObject;

namespace Domain.Transactions.Agreggates;
public class Transaction : Base
{
    public DateTime DtTransaction { get; set; }
    public Monetary Value { get; set; } = 0;
    public String? Description { get; set; }
    public virtual Customer? Customer { get; set; }
    public Guid CorrelationId { get; set; }
}
