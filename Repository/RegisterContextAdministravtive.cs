using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Domain.Admin.Agreggates;

namespace Repository;
public class RegisterContextAdministravtive: DbContext
{
    public RegisterContextAdministravtive(DbContextOptions<RegisterContextAdministravtive> options) : base(options) { }
    public DbSet<AdministrativeAccount> Admin{ get; set; }    
   protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(RegisterContextAdministravtive).Assembly);
        base.OnModelCreating(modelBuilder);
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseLoggerFactory(LoggerFactory.Create(x => x.AddConsole()));
        base.OnConfiguring(optionsBuilder);
    }
}