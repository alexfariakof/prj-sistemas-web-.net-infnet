using Domain.Account.Agreggates;
using Domain.Core.Aggreggates;

namespace Domain.Streaming.Agreggates;
public class Album : BaseModel
{
    public string Name { get; set; }
    public virtual List<Music> Music { get; set; } = new List<Music>();
    public virtual List<PlaylistPersonal> MusicPersonal { get; set; } = new List<PlaylistPersonal>();
    public void AddMusic(Music music) => this.Music.Add(music);
    public void AddMusic(List<Music> music) => this.Music.AddRange(music);    
    public void AddMusic(PlaylistPersonal music) => this.MusicPersonal.Add(music);
    public void AddMusic(List<PlaylistPersonal> music) => this.MusicPersonal.AddRange(music);
}
