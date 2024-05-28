using Microsoft.EntityFrameworkCore;
using Repository.Abastractions;

namespace Migrations.MySqlServer;
public class MySqlServerContextAdministrative : BaseRegisterContextAdministravtive<MySqlServerContextAdministrative>
{
    public MySqlServerContextAdministrative(DbContextOptions<MySqlServerContextAdministrative> options) : base(options) { }
}