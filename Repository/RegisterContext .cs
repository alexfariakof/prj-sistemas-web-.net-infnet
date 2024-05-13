using Microsoft.EntityFrameworkCore;

namespace Repository;
public class RegisterContext : BaseContext<RegisterContext>
{
    public RegisterContext(DbContextOptions<RegisterContext> options) : base(options) { }
}
