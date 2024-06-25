using Domain.Core.Aggreggates;
using System.ComponentModel.DataAnnotations;

namespace Application.Streaming.Dto;
public class GenreDto : BaseDto
{
    [Required(ErrorMessage = "O campo Nome é obrigatório.")]
    public string? Name { get; set; }    
}