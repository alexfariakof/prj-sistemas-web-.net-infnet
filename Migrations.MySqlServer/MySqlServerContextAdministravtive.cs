using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Domain.Admin.Agreggates;
using Repository.Mapping.Admin;

namespace Migrations.MySqlServer;
public class MySqlServerContextAdministravtive: DbContext
{
    public MySqlServerContextAdministravtive(DbContextOptions<MySqlServerContextAdministravtive> options) : base(options) { }
    public DbSet<AdministrativeAccount> Admin { get; set; }    
   protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new AdminAccountMap());
        base.OnModelCreating(modelBuilder);
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseLoggerFactory(LoggerFactory.Create(x => x.AddConsole()));
        base.OnConfiguring(optionsBuilder);
    }
}