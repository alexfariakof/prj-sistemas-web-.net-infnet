using Domain.Streaming.Agreggates;
using Domain.Streaming.ValueObject;
using Repository;

namespace DataSeeders.Implementations;
public class DataSeederMusic : IDataSeeder
{
    private readonly RegisterContext _context;
    public DataSeederMusic(RegisterContext context)
    {
        _context = context;
    }
    public void SeedData()
    {
        var band = new Band
        {
            Name = "Example Band",
            Description = "An example band description",
            Backdrop = "band-backdrop.jpg"
        };


        var playlist = new Playlist
        {
            Name = "Example Playlist",
            Flats = _context.Flat.ToList(),
        };

        var musicList = new List<Music>
            {
                new Music
                {
                    Name = "Song 1",
                    Duration = new Duration(30),
                    Playlists = { playlist },
                    Flats = _context.Flat.Where(f => f.Id == new Guid("3fa85f64-5717-4562-b3fc-2c963f66afa5")).ToList()
                },
                new Music
                {
                    Name = "Song 2",
                    Duration = new Duration(15),
                    Playlists = { playlist },
                    Flats = _context.Flat.Where(f => f.Id == new Guid("3fa85f64-5717-4562-b3fc-2c963f66afa5")).ToList()
                },
                new Music
                {
                    Name = "Song 3",
                    Duration = new Duration(45),
                    Playlists = { playlist },
                    Flats = _context.Flat.Where(f => f.Id == new Guid("3fa85f64-5717-4562-b3fc-2c963f66afa5")).ToList()
                },
                new Music
                {
                    Name = "Song 4",
                    Duration = new Duration(5),
                    Playlists = { playlist },
                    Flats = _context.Flat.Where(f => f.Id == new Guid("3fa85f64-5717-4562-b3fc-2c963f66afa5")).ToList()
                },
                new Music
                {
                    Name = "Song 5",
                    Duration = new Duration(55),
                    Playlists = { playlist },
                    Flats = _context.Flat.Where(f => f.Id == new Guid("3fa85f64-5717-4562-b3fc-2c963f66afa5")).ToList()
                }
            };

        var album = new Album
        {
            Name = "Example Album",
            Musics = musicList,
            Flats = _context.Flat.ToList()
        };

        band.AddAlbum(album);
        musicList.ForEach(m => m.Album = album);

        try
        {
            _context.Music.AddRange(musicList);
            _context.Add(album);
            _context.Add(band);
            _context.SaveChanges();
        }
        catch
        {
            _context.Music.RemoveRange(musicList);
            Console.WriteLine($"Dados já cadastrados na base de dados");
        }
    }
}