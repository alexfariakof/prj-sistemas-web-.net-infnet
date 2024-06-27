using Domain.Core.Aggreggates;
using System.ComponentModel.DataAnnotations;

namespace Application.Streaming.Dto;
public class AlbumDto: BaseDto
{

    [Required(ErrorMessage = "O campo Nome é obrigatório.")]
    public string? Name { get; set; }

    [Required]
    public Guid FlatId { get; set; }
    
    [Required]
    public Guid BandId { get; set; }

    [Required(ErrorMessage = "O campo Backdrop é obrigatório.")]
    [Url(ErrorMessage = "Está não é uma url válida.")]
    public string? Backdrop { get; set; }
    
    [Required(ErrorMessage = "No mínimo uma música deve ser adiconada ao album.")]
    public List<MusicDto>? Musics { get; set; }
}