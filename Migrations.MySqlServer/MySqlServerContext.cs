using Microsoft.EntityFrameworkCore;
using Repository;

namespace Migrations.MySqlServer;

public class MySqlServerContext : BaseContext<MySqlServerContext>
{
    public MySqlServerContext(DbContextOptions<MySqlServerContext> options) : base(options) { }
}
