using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Domain.Admin.Agreggates;
using Repository.Mapping.Admin;
using Domain.Core.ValueObject;

namespace Repository;
public class RegisterContextAdministravtive: DbContext
{
    public RegisterContextAdministravtive(DbContextOptions<RegisterContextAdministravtive> options) : base(options) { }
    public DbSet<AdminAccount> Admin{ get; set; }    
   protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new AdminAccountMap()).Entity<Login>().HasNoKey();
        base.OnModelCreating(modelBuilder);
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseLoggerFactory(LoggerFactory.Create(x => x.AddConsole()));
        base.OnConfiguring(optionsBuilder);
    }
}