using Application.Streaming.Dto;

namespace LiteStreaming.AdministrativeApp.Models;

public class AlbumViewModel
{
    public AlbumDto? Album { get; set; }
    public List<GenreDto>? Genres { get; set; }
    public List<BandDto>? Bands { get; set; }
}