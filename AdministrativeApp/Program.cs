using AdministrativeApp.CommonDependenceInject;
using Microsoft.EntityFrameworkCore;
using Migrations.MsSqlServer.CommonInjectDependence;
using Migrations.MySqlServer.CommonInjectDependence;
using Application.CommonInjectDependence;
using Repository.CommonInjectDependence;
using Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRepositoriesAdministrativeApp();
builder.Services.AddServicesAdministrativeApp();
builder.Services.AddAutoMapperAdministrativeApp();

if (builder.Environment.IsDevelopment())
{
    builder.Services.AddDataSeeders();
    //builder.Services.AddDbContext<RegisterContextAdministravtive>(opt => opt.UseLazyLoadingProxies().UseInMemoryDatabase("Register_Database_Administrative_InMemory"));
    builder.Services.AddDbContext<RegisterContextAdministravtive>(options => options.UseLazyLoadingProxies().UseSqlServer(builder.Configuration.GetConnectionString("MsSqlAdministrativeConnectionString")));
    builder.Services.ConfigureMsSqlServerMigrationsContext(builder.Configuration);
    builder.Services.ConfigureMySqlServerMigrationsContext(builder.Configuration);
}

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");    
    app.UseHsts();    
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}");

if (app.Environment.IsDevelopment())
    app.RunDataSeeders();

app.Run();
