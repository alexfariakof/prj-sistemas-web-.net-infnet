using Microsoft.EntityFrameworkCore;
using Repository.Abastractions;

namespace Migrations.MySqlServer;
public class MySqlServerContextAdministrative : BaseContextAdministravtive<MySqlServerContextAdministrative>
{
    public MySqlServerContextAdministrative(DbContextOptions<MySqlServerContextAdministrative> options) : base(options) { }
}