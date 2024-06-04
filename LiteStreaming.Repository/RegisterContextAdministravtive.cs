using Microsoft.EntityFrameworkCore;
using Repository.Abastractions;

namespace Repository;
public class RegisterContextAdministravtive: BaseContextAdministravtive<RegisterContextAdministravtive>
{
    public RegisterContextAdministravtive(DbContextOptions<RegisterContextAdministravtive> options) : base(options) { }
}