using Microsoft.EntityFrameworkCore;
using Repository.Abstractions;

namespace Repository;
public class RegisterContextAdmin: BaseContextAdministrative<RegisterContextAdmin>
{
    public RegisterContextAdmin(DbContextOptions<RegisterContextAdmin> options) : base(options) { }
}