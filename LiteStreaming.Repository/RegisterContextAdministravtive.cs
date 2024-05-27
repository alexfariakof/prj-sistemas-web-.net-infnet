using Microsoft.EntityFrameworkCore;
using Repository.Abastractions;

namespace Repository;
public class RegisterContextAdministravtive: BaseRegisterContextAdministravtive<RegisterContextAdministravtive>
{
    public RegisterContextAdministravtive(DbContextOptions<RegisterContextAdministravtive> options) : base(options) { }
}