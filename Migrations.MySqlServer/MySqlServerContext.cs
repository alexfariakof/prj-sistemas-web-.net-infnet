using Microsoft.EntityFrameworkCore;
using Repository.Abstractions;

namespace Migrations.MySqlServer;
public class MySqlServerContext : BaseContext<MySqlServerContext>
{
    public override BaseConstants BASE_CONSTS { get;  } = new BaseConstants();

    public MySqlServerContext(DbContextOptions<MySqlServerContext> options) : base(options)
    {
        this.BASE_CONSTS.CURRENT_DATE = "Now()";
    }
}
