using Domain.Core.Aggreggates;
using Domain.Streaming.Agreggates;

namespace Domain.Account.Agreggates
{
    public class Signature : BaseModel
    {
        public Flat Flat { get; set; }
        public Boolean Active { get; set; }
        public DateTime DtActivation { get; set; }
    }
}
