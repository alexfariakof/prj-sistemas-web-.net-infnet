using LiteStreaming.Cryptography;
using LiteStreaming.STS;
using LiteStreaming.STS.Data;
using LiteStreaming.STS.GrantType;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.Configure<DataBaseoptions>(builder.Configuration.GetSection("ConnectionStrings"));
builder.Services.Configure<CryptoOptions>(builder.Configuration.GetSection("Crypto"));
builder.Services.AddSingleton<ICrypto, Crypto>();
builder.Services.AddScoped<IIdentityRepository, IdentityRepository>();
builder.Services
    .AddIdentityServer()
    .AddDeveloperSigningCredential()
    .AddInMemoryIdentityResources(IdentityServerConfigurations.GetIdentityResource())
    .AddInMemoryApiResources(IdentityServerConfigurations.GetApiResources())
    .AddInMemoryApiScopes(IdentityServerConfigurations.GetApiScopes())
    .AddInMemoryClients(IdentityServerConfigurations.GetClients())
    .AddResourceOwnerValidator<ResourceOwnerPasswordValidator>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseIdentityServer();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.Run();
