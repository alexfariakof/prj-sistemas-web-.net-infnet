using Domain.Core.Aggreggates;
using System.ComponentModel.DataAnnotations;

namespace Application.Streaming.Dto;
public class BandDto: BaseDto
{    
    [Required(ErrorMessage = "O campo Nome é obrigatório.")]
    public string? Name { get; set; }

    [Required(ErrorMessage = "O campo Descrição é obrigatório.")]
    public string? Description { get; set; }

    [Required(ErrorMessage = "O campo Backdrop é obrigatório.")]
    [Url(ErrorMessage = "Está não é uma url válida.")]
    public string? Backdrop { get; set; }
    public List<AlbumDto>? Albums { get; set; }
}