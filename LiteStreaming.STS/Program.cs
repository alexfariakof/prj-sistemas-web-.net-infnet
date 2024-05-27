using LiteStreaming.Cryptography;
using LiteStreaming.STS;
using LiteStreaming.STS.Data;
using LiteStreaming.STS.GrantType;
using LiteStreaming.STS.ProfileService;
using LiteStreaming.STS.SwaggerUIDocumentation;
using Microsoft.OpenApi.Models;

// Appliction Parameteres
var appName = "Security Token Service";
var currentVersion = "v1";
var appDescription = $"Esta API é um componente de Serviços de Token de Segurança que emite tokens de segurança para autenticar e autorizar solicitações de usuários. ";

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc(currentVersion, new OpenApiInfo
    {
        Title = appName,
        Version = currentVersion,
        Description = appDescription,
        Contact = new OpenApiContact
        {
            Name = "Alex Ribeiro de Faria - Projeto Security Token Service",
            Url = new Uri("https://github.com/alexfariakof/prj-sistemas-web-.net-infnet/tree/main/LiteStreaming.STS")
        }
    });

    c.AddDocumentFilterInstance<AuthenticationOperationFilter>(new AuthenticationOperationFilter());
});


builder.Services.Configure<DataBaseoptions>(builder.Configuration.GetSection("ConnectionStrings"));
builder.Services.Configure<CryptoOptions>(builder.Configuration.GetSection("Crypto")).AddSingleton<ICrypto, Crypto>();

builder.Services.AddScoped<IIdentityRepository, IdentityRepository>();
builder.Services
    .AddIdentityServer()
    .AddDeveloperSigningCredential()
    .AddInMemoryIdentityResources(IdentityServerConfigurations.GetIdentityResource())
    .AddInMemoryApiResources(IdentityServerConfigurations.GetApiResources())
    .AddInMemoryApiScopes(IdentityServerConfigurations.GetApiScopes())
    .AddInMemoryClients(IdentityServerConfigurations.GetClients())
    .AddResourceOwnerValidator<ResourceOwnerPasswordValidator>()
    .AddProfileService<ProfileService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
/*if (app.Environment.IsDevelopment())
{*/
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint($"/swagger/{currentVersion}/swagger.json", $"{currentVersion} {appName} ");
    });
/*}*/
app.UseHsts();
app.UseHttpsRedirection();
app.UseIdentityServer();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
