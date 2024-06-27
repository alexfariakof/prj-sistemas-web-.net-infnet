using Application.Streaming.Dto;

namespace LiteStreaming.AdministrativeApp.Models;

public class MusicViewModel
{
    public MusicDto? Music { get; set; }
    public List<GenreDto>? Genres { get; set; }
    public List<BandDto>? Bands { get; set; }
    public List<AlbumDto>? Albums { get; set; }
}