using Dapper;
using LiteStreaming.STS.Model;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;

namespace LiteStreaming.STS.Data;

internal class IdentityRepository : IIdentityRepository
{
    private readonly string connectionString;

    public IdentityRepository(IOptions<DataBaseoptions> options)
    {
        this.connectionString = options.Value.DefaultConnectionString;
    }

    public async Task<User> FindByIdAsync(Guid Id)
    {
        using (var connection = new SqlConnection(this.connectionString))
        {
            var user = await connection.QueryFirstAsync<User>(IdentityQuery.FindById(), new { id = Id });
            return user;
        }
    }

    public async Task<User> FindByEmail(string email)
    {
        using (var connection = new SqlConnection(this.connectionString))
        {
            var user = await connection.QueryFirstOrDefaultAsync<User>(IdentityQuery.FindByEmail(), new { email = email });
            return user;
        }
    }
}

internal static class IdentityQuery
{
    public static string FindById() => 
        @"SELECT [Id]
                ,[PerfilTypeId]
                ,[Email]
          FROM [dbo].[User] 
          WHERE id = @id";

    public static string FindByEmail() =>
        @"SELECT [Id]
                ,[PerfilTypeId]
                ,[Email]
                ,[Password]
          FROM [dbo].[User] 
          WHERE email = @email";
}