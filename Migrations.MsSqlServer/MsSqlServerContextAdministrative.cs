using Microsoft.EntityFrameworkCore;
using Repository.Abastractions;

namespace Migrations.MsSqlServer;
public class MsSqlServerContextAdministrative : BaseContextAdministravtive<MsSqlServerContextAdministrative>
{
    public MsSqlServerContextAdministrative(DbContextOptions<MsSqlServerContextAdministrative> options) : base(options) { }
}