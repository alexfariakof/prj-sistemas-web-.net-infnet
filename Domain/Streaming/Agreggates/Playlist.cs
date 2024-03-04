using Domain.Core.Aggreggates;

namespace Domain.Streaming.Agreggates;
public class Playlist : BaseModel
{
    public string? Name { get; set; }
    public virtual IList<Flat> Flats { get; set; } = new List<Flat>();
    public virtual IList<Music> Musics { get; set; } = new List<Music>();
} 
