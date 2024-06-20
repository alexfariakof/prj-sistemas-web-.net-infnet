using AdministrativeApp.CommonDependenceInject;
using Microsoft.EntityFrameworkCore;
using Migrations.MsSqlServer.CommonInjectDependence;
using Migrations.MySqlServer.CommonInjectDependence;
using Application.CommonInjectDependence;
using Repository.CommonInjectDependence;
using Repository;
using Microsoft.AspNetCore.Localization;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRepositoriesAdministrativeApp();
builder.Services.AddServicesAdministrativeApp();
builder.Services.AddAutoMapperAdministrativeApp();
builder.Services.AddServices();

// Cryptography 
builder.Services.AddServicesCryptography(builder.Configuration);


if (builder.Environment.IsDevelopment())
{
    builder.Services.AddDataSeeders();
    builder.Services.AddDbContext<RegisterContextAdministravtive>(options => options.UseLazyLoadingProxies().UseSqlServer(builder.Configuration.GetConnectionString("MsSqlAdministrativeConnectionString")));
    builder.Services.AddDbContext<RegisterContext>(options => options.UseLazyLoadingProxies().UseSqlServer(builder.Configuration.GetConnectionString("MsSqlConnectionString")));
    builder.Services.ConfigureMsSqlServerMigrationsContext(builder.Configuration);
    builder.Services.ConfigureMySqlServerMigrationsContext(builder.Configuration);
}
else if (builder.Environment.IsProduction())
{
    builder.Services.AddDbContext<RegisterContextAdministravtive>(options => options.UseLazyLoadingProxies().UseSqlServer(builder.Configuration.GetConnectionString("MsSqlAdministrativeConnectionString")));
}
else
{
    builder.Services.AddDataSeeders();
    builder.Services.AddDbContext<RegisterContextAdministravtive>(opt => opt.UseLazyLoadingProxies().UseInMemoryDatabase("Register_Database_Administrative_InMemory"));
}

var app = builder.Build();

var supportedCultures = new[] { new CultureInfo("pt-BR") };
var localizationOptions = new RequestLocalizationOptions
{
    DefaultRequestCulture = new RequestCulture("pt-BR"),
    SupportedCultures = supportedCultures,
    SupportedUICultures = supportedCultures
};
app.UseRequestLocalization(localizationOptions);

// Configure the HTTP request pipeline.
if (app.Environment.IsProduction())
{
    app.UseExceptionHandler("/Home/Error");    
    app.UseHsts();    
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}");

if (!app.Environment.IsProduction())
    app.RunDataSeeders();

app.UseSession();
app.Run();
