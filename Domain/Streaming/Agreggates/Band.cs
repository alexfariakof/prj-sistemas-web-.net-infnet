using Domain.Core.Aggreggates;

namespace Domain.Streaming.Agreggates;
public class Band : BaseModel
{
    public String? Name { get; set; }
    public String? Description { get; set; }
    public String? Backdrop { get; set; }
    public virtual List<Album> Albums { get; set; } = new List<Album>();
    public void AddAlbum(Album album) => this.Albums.Add(album);
}
