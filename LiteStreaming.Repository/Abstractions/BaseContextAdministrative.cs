using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Domain.Administrative.Agreggates;
using Repository.Mapping.Administrative;
using Domain.Administrative.ValueObject;

namespace Repository.Abstractions;
public class BaseContextAdministrative<TContext> : DbContext where TContext : DbContext
{
    public BaseContextAdministrative(DbContextOptions<TContext> options) : base(options) { }
    public DbSet<AdminAccount> Admin { get; set; }
    public DbSet<Perfil> Perfil { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new AdminAccountMap());
        modelBuilder.ApplyConfiguration(new PerfilMap());
        base.OnModelCreating(modelBuilder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseLoggerFactory(LoggerFactory.Create(x => x.AddConsole()));
        base.OnConfiguring(optionsBuilder);
    }
}