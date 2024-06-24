using Microsoft.EntityFrameworkCore;
using Repository.Abstractions;

namespace Repository;
public class RegisterContextAdministrative: BaseContextAdministrative<RegisterContextAdministrative>
{
    public RegisterContextAdministrative(DbContextOptions<RegisterContextAdministrative> options) : base(options) { }
}