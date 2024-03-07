using Application.Account.Dto;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Application.Account.Dto;
public class PlaylistPersonalDto : IValidatableObject
{
    public Guid? Id { get; set; }
    public string? Name { get; set; }
    public IList<MusicDto> Musics { get; set; } = new List<MusicDto>();

    [JsonIgnore]
    public Guid CustumerId { get; set; }
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (validationContext.Items.TryGetValue("HttpMethod", out var httpMethod) && httpMethod is string method)
        {
            if (method.Equals("POST", StringComparison.OrdinalIgnoreCase) && string.IsNullOrEmpty(Name))
            {
                yield return new ValidationResult("Name is required for create a playlist.", new[] { nameof(Name) });
            }

            if (method.Equals("PUT", StringComparison.OrdinalIgnoreCase) && Id == null)
            {
                yield return new ValidationResult("ID is required for update a playlist.", new[] { nameof(Id) });
            }

            if (method.Equals("DELETE", StringComparison.OrdinalIgnoreCase) && Id == null)
            {
                yield return new ValidationResult("ID is required for delete a playlist.", new[] { nameof(Id) });
            }
        }
    }
}