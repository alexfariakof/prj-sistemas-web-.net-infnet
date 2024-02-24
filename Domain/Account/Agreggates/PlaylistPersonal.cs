using Domain.Core.Aggreggates;
using Domain.Streaming.Agreggates;

namespace Domain.Account.Agreggates
{
    public class PlaylistPersonal : BaseModel
    {
        public Customer Customer { get; set; }
        public bool IsPublic { get; set; }
        public DateTime DtCreated { get; set; }
        public string Name { get; set; }
        public List<Music<PlaylistPersonal>> Musics { get ; set ; } = new List<Music<PlaylistPersonal>>();
    }
}
