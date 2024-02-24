using Domain.Core.Aggreggates;
using Domain.Streaming.ValueObject;

namespace Domain.Streaming.Agreggates
{
    public class Music<T> : BaseModel
    {
        public String Name { get; set; }
        public Duration Duration { get; set; }
        public List<T> Playlists { get; set; } = new List<T>();
    }
}
