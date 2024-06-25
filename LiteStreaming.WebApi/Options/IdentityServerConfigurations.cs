namespace LiteStreaming.WebApi.Options;
public  class IdentityServerConfigurations
{
    public string Authority { get; set; }
    public string? ApiName { get; set; }
    public string? ApiSecret { get; set; }
    public bool RequireHttpsMetadata { get; set; }
}
