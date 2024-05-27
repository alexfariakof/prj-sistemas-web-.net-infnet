using LiteStreaming.STS.Model;

namespace LiteStreaming.STS.Data;

internal interface IIdentityRepository
{
    Task<User> FindByEmail(string email);
    Task<User> FindByIdAsync(Guid Id);
}