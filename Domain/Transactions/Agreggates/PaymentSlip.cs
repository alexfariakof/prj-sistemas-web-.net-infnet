using Domain.Core.Aggreggates;
using Domain.Core.ValueObject;
using Domain.Transactions.ValuesObject;

namespace Domain.Payments.Agreggates;
public class PaymentSlip : BaseModel
{
    public DateTime Date { get; set; }
    public Status Status { get; set; }
    public Guid CorrelationId { get; set; }
    public Monetary Monetary { get; set; } = 0;
}
