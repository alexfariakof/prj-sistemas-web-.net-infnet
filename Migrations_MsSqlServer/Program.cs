using Repository;
using Microsoft.EntityFrameworkCore;
using DataSeeders;
using DataSeeders.Implementations;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<RegisterContext>(options =>
options
.UseLazyLoadingProxies(true)
.UseSqlServer(
    builder.Configuration.GetConnectionString("MsSqlServerConnectionString"), 
    b => b.MigrationsAssembly("Migrations_MsSqlServer")));

builder.Services.AddTransient<IDataSeeder, DataSeeder>();

try
{
    var app = builder.Build();
    app.MapGet("/", () => "Migrations MsSqlServer!");

    using (var scope = app.Services.CreateScope())
    {
        var services = scope.ServiceProvider;
        var dataSeeder = services.GetRequiredService<IDataSeeder>();
        dataSeeder.SeedData();
    }

    app.Run();
}
catch(Exception ex)
{
    Console.WriteLine(ex.Message);
}