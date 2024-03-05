using System.ComponentModel.DataAnnotations;

namespace Application.Streaming.Dto;
public class MusicDto
{
    public Guid Id { get; set; }

    [Required]
    public String? Name { get; set; }
    public int Duration { get; set; } = 0;

    [Required]
    public Guid FlatId { get; set; }
    public Guid AlbumId { get; set; }
}