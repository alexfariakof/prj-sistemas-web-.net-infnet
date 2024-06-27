using Domain.Core.Aggreggates;
using System.ComponentModel.DataAnnotations;

namespace Application.Streaming.Dto;
public class MusicDto : BaseDto
{
    [Required(ErrorMessage = "O campo Nome é obrigatório.")]
    public string? Name { get; set; }

    [Required(ErrorMessage = "O campo Url é obrigatório.")]
    [Url(ErrorMessage = "Está não é uma url válida.")]
    public string? Url { get; set; }

    [Required(ErrorMessage = "A duração é obrigatória.")]
    [Range(1, int.MaxValue, ErrorMessage = "A duração deve ser maior que zero.")]
    public int Duration { get; set; } = 0;
    public Guid FlatId { get; set; }
    
    [Required]
    public Guid AlbumId { get; set; }

    [Required]
    public Guid GenreId {get; set; }
    public string? AlbumBackdrop { get; set; }
    public string? AlbumName { get; set; }
    
    [Required]
    public Guid BandId { get; set; }
    public string? BandBackDrop { get; set; }
    public string? BandName{ get; set; }
    public string? BandDescription { get; set; }
}