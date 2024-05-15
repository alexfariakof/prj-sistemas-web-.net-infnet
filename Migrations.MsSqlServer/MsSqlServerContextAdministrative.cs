using Microsoft.EntityFrameworkCore;
using Repository.Abastractions;

namespace Migrations.MsSqlServer;
public class MsSqlServerContextAdministrative : BaseRegisterContextAdministravtive<MsSqlServerContextAdministrative>
{
    public MsSqlServerContextAdministrative(DbContextOptions<MsSqlServerContextAdministrative> options) : base(options) { }
}