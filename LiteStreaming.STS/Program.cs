using EasyCryptoSalt;
using LiteStreaming.STS;
using LiteStreaming.STS.Data;
using LiteStreaming.STS.Data.Interfaces;
using LiteStreaming.STS.Data.Options;
using LiteStreaming.STS.GrantType;
using LiteStreaming.STS.ProfileService;
using LiteStreaming.STS.SwaggerUIDocumentation;
using Microsoft.OpenApi.Models;

// Appliction Parameteres
var appName = "Security Token Service";
var currentVersion = "v1";
var appDescription = $"Esta API � um componente de Servi�os de Token de Seguran�a que emite tokens de seguran�a para autenticar e autorizar solicita��es de usu�rios. ";

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCors(c =>
{
    c.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();

    });
});
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

builder.Services.AddMvc();
builder.Services.AddControllersWithViews();
builder.Services.Configure<DataBaseOptions>(builder.Configuration.GetSection("ConnectionStrings"));
builder.Services.Configure<CryptoOptions>(builder.Configuration.GetSection("CryptoConfigurations")).AddSingleton<ICrypto, Crypto>();
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

builder.Services.AddAuthentication();
builder.Services.AddAuthorization();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint($"/swagger/{currentVersion}/swagger.json", $"{currentVersion} {appName} ");
    });
    app.UseDeveloperExceptionPage();
}

app.UseStaticFiles();
app.UseCors();
app.UseIdentityServer();
app.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();

// https://localhost:7199/.well-known/openid-configuration
// https://github.com/identityServer/IdentityServer4.Quickstart.UI