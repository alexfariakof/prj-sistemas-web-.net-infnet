using Repository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<RegisterContext>(options =>
options.UseMySQL(builder.Configuration.GetConnectionString("MySqlConnectionString"),
b => b.MigrationsAssembly("Migrations_MySqlServer")));

var app = builder.Build();
app.MapGet("/", () => "Migrations Mysql Server!");
app.Run();