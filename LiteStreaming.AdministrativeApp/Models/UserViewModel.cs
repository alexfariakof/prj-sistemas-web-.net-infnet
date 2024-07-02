using Application.Administrative.Dto;

namespace LiteStreaming.AdministrativeApp.Models;
public class UserViewModel
{
   public AdminAccountDto? Accout { get; set; }
   public IEnumerable<AdminAccountDto>? Items { get; set; }
   public PagerModel? PageModel { get; set; }
}
