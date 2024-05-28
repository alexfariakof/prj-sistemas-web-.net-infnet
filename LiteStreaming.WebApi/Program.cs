using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Migrations.MySqlServer.CommonInjectDependence;
using Migrations.MsSqlServer.CommonInjectDependence;
using Application.CommonInjectDependence;
using Repository.CommonInjectDependence;
using WebApi.CommonInjectDependence;
using Repository;

var builder = WebApplication.CreateBuilder(args);

// Appliction Parameteres
var appName = "Lite Streaming API";
var appVersion = "v1";
var appDescription = $"API Lite Streaming Serviços de Streaming.";

// Add services to the container.
builder.Services.AddRouting(options => options.LowercaseUrls = true);
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => {
    c.SwaggerDoc(appVersion,
    new OpenApiInfo
    {
        Title = appName,
        Version = appVersion,
        Description = appDescription,
        Contact = new OpenApiContact
        {
            Name = "Alex Ribeiro de Faria - Projeto Web API .Net Core Lite Streaming ",
            Url = new Uri("https://github.com/alexfariakof/prj-sistemas-web-.net-infnet/tree/main/LiteStreaming.WebApi")
        }
    });
});

builder.Services.AddDataSeeders();

if (builder.Environment.IsStaging())
{    
    builder.Services.AddDbContext<RegisterContext>(opt => opt.UseLazyLoadingProxies().UseInMemoryDatabase("Register_Database_InMemory"));    
}
else if (builder.Environment.IsDevelopment())
{    
    builder.Services.AddDbContext<RegisterContext>(options => options.UseLazyLoadingProxies().UseSqlServer(builder.Configuration.GetConnectionString("MsSqlConnectionString")));
    builder.Services.AddDbContext<RegisterContextAdministravtive>();
    builder.Services.ConfigureMsSqlServerMigrationsContext(builder.Configuration);
    builder.Services.ConfigureMySqlServerMigrationsContext(builder.Configuration);
}
else if (builder.Environment.IsProduction())
{
    builder.Services.AddDbContext<RegisterContext>(options => options.UseLazyLoadingProxies().UseSqlServer(builder.Configuration.GetConnectionString("MsSqlConnectionString")));
}

// Autorization Configuratons
builder.Services.AddAuthConfigurations(builder.Configuration);

// AutoMapper
builder.Services.AddAutoMapperWebApiApp();

// Repositories
builder.Services.AddRepositoriesWebApiApp();

// Services
builder.Services.AddServicesWebApiApp();

// Cryptography 
builder.Services.AddServicesCryptography(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsStaging())
{    
    app.Urls.Add("http://0.0.0.0:5146");
    app.Urls.Add("https://0.0.0.0:7204");
}

app.UseDefaultFiles();
app.UseStaticFiles();

if (app.Environment.IsDevelopment() || app.Environment.IsStaging())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", $"{appName} {appVersion}"); });
    app.RunDataSeeders();
}
else
{
    app.UseHttpsRedirection();
}
    
app.UseAuthorization();
app.MapControllers();

if (app.Environment.IsProduction() || app.Environment.IsStaging())
    app.MapFallbackToFile("/index.html");

app.Run();