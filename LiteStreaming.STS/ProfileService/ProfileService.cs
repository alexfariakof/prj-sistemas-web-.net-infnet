using IdentityServer4.Extensions;
using IdentityServer4.Models;
using IdentityServer4.Services;
using LiteStreaming.STS.Data.Interfaces;
using System.Security.Claims;

namespace LiteStreaming.STS.ProfileService;

internal class ProfileService: IProfileService
{
    private readonly IIdentityRepository repository;

    public ProfileService(IIdentityRepository repository) => this.repository = repository;

    public async Task GetProfileDataAsync(ProfileDataRequestContext context)
    {
        var id = context.Subject.GetSubjectId();
        var user = await this.repository.FindByIdAsync(new Guid(id));

        var claims = new List<Claim>()
        {
            new Claim("iss", "http://localhost:5055"),
            new Claim("UserId", user.Id.ToString()),
            new Claim("email", user.Email),
            new Claim("role", user.UserType.ToString())
        };
        context.IssuedClaims = claims;
    }

    public Task IsActiveAsync(IsActiveContext context)
    {
        context.IsActive = true;
        return Task.CompletedTask;
    }
}
