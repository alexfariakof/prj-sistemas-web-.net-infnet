using Microsoft.EntityFrameworkCore;
using Repository.Abastractions;

namespace Repository;
public class RegisterContext : BaseContext<RegisterContext>
{
    public override BaseConstants BASE_CONSTS { get; } = new BaseConstants();
    public RegisterContext(DbContextOptions<RegisterContext> options) : base(options) { }
}
