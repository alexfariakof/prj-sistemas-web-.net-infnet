using Domain.Core.Aggreggates;

namespace Domain.Streaming.Agreggates;
public class Genre : BaseModel
{
    public string? Name { get; set; }    
    public virtual IList<Music> Musics { get; set; } = new List<Music>();
}