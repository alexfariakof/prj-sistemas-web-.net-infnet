using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Domain.Administrative.Agreggates;
using Repository.Mapping.Administrative;
using Domain.Administrative.ValueObject;

namespace Repository.Abstractions;
public class BaseContextAdministrative<TContext> : BaseContext<TContext> where TContext : DbContext
{
    public override BaseConstants BASE_CONSTS { get; } = new BaseConstants();
    public BaseContextAdministrative(DbContextOptions<TContext> options) : base(options) { }
    public DbSet<AdministrativeAccount> Admin { get; set; }
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