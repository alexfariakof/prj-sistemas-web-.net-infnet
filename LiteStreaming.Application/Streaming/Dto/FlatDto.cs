using Domain.Core.Aggreggates;
using System.ComponentModel.DataAnnotations;

namespace Application.Streaming.Dto;
public class FlatDto: BaseDto
{
    [Required]
    public string? Name { get; set; }

    [Required]
    public string? Description { get; set; }

    [Required]
    public decimal Value { get; set; }
}