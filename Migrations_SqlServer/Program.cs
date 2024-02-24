using Repository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<RegisterContext>(options => 
options.UseSqlServer(
    builder.Configuration.GetConnectionString("SqlServerConnectionString"), 
    b => b.MigrationsAssembly("Migrations_SqlServer")));

var app = builder.Build();
app.MapGet("/", () => "Migrations SqlServer!");
app.Run();