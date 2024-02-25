using Repository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<RegisterContext>(options => 
options.UseSqlServer(
    builder.Configuration.GetConnectionString("SqlServerConnectionString"), 
    b => b.MigrationsAssembly("Migrations_MsSqlServer")));

var app = builder.Build();
app.MapGet("/", () => "Migrations MsSqlServer!");
app.Run();