using Domain.Administrative.Agreggates;
using Repository.Abastractions;
using Repository.Interfaces;

namespace Repository.Persistency;
public class AdminAccountRepository : BaseRepository<AdministrativeAccount>, IRepository<AdministrativeAccount>
{
    public RegisterContextAdministravtive Context { get; set; }
    public AdminAccountRepository(RegisterContextAdministravtive context) : base(context)
    {
        Context = context;
    }   
}