using Repository.Constants;
using Microsoft.EntityFrameworkCore;
using Repository.Abstractions;

namespace Repository;
public class RegisterContext : BaseContext<RegisterContext>
{
    public override BaseConstants BASE_CONSTS { get; } = new BaseConstants();
    public RegisterContext(DbContextOptions<RegisterContext> options) : base(options) { }
}
