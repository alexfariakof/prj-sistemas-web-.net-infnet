using System.ComponentModel.DataAnnotations;

namespace Application.Account.Dto;
public class BandDto
{
    public Guid Id { get; set; }
    
    [Required]
    public String Name { get; set; }

    [Required]
    public String Description { get; set; }

    [Required]
    public String Backdrop { get; set; }
    public AlbumDto Album { get; set; }
}