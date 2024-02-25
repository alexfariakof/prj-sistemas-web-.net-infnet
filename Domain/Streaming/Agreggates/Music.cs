using Domain.Account.Agreggates;
using Domain.Core.Aggreggates;
using Domain.Streaming.ValueObject;

namespace Domain.Streaming.Agreggates;
public class Music : BaseModel
{
    public String? Name { get; set; }
    public Duration Duration { get; set; } = 0;
    public virtual IList<PlaylistPersonal> PersonalPlaylists { get; set; } = new List<PlaylistPersonal>();
    public virtual IList<Playlist> Playlists { get; set; } = new List<Playlist>();
}