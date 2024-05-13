using Domain.Admin.Agreggates;
using Repository.Interfaces;

namespace Repository.Repositories;
public class AdminAccountRepository : RepositoryBase<AdministrativeAccount>, IRepository<AdministrativeAccount>
{
    public RegisterContextAdministravtive Context { get; set; }
    public AdminAccountRepository(RegisterContextAdministravtive context) : base(context)
    {
        Context = context;
    }   
}