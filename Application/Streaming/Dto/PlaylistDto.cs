using System.ComponentModel.DataAnnotations;

namespace Application.Streaming.Dto;
public class PlaylistDto
{
    public Guid Id { get; set; }

    [Required]
    public string? Name { get; set; }
    public IList<MusicDto> Musics { get; set; } = new List<MusicDto>();

}