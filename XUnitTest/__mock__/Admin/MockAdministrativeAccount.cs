using Domain.Administrative.Agreggates;
using Bogus;
using Domain.Administrative.ValueObject;
using Application.Administrative.Dto;

namespace __mock__.Admin;
public class MockAdministrativeAccount
{
    private static MockAdministrativeAccount _instance;
    private static readonly object LockObject = new object();
    public static MockAdministrativeAccount Instance
    {
        get
        {
            lock (LockObject)
            {
                return _instance ??= new MockAdministrativeAccount();
            }
        }
    }

    private MockAdministrativeAccount() { }

    public AdministrativeAccount GetFaker()
    {
        var fakeAdministrativeAccount = new Faker<AdministrativeAccount>()
            .RuleFor(a => a.Id, f => f.Random.Guid())
            .RuleFor(a => a.Login, MockLogin.Instance.GetFaker())
            .RuleFor(a => a.DtCreated, f => f.Date.Past())
            .RuleFor(a => a.Name, f => f.Name.FirstName())
            .RuleFor(a => a.PerfilType, f => new Perfil(Perfil.UserType.Admin));

        return fakeAdministrativeAccount;
    }
    
    public List<AdministrativeAccount> GetListFaker(int count)
    {
        var administrativeAccounts = new List<AdministrativeAccount>();

        for (int i = 0; i < count; i++)
        {
            administrativeAccounts.Add(GetFaker());
        }

        return administrativeAccounts;
    }
    
    public AdministrativeAccountDto GetNewDtoFromAdministrativeAccount(AdministrativeAccount account)
    {
        lock (LockObject)
        {
            var fakeAdministrativeAccountDto = new Faker<AdministrativeAccountDto>()
                .RuleFor(a => a.Id, f => account.Id)
                .RuleFor(a => a.Name, f => account.Name)
                .RuleFor(a => a.Email, f => account.Login.Email)
                .RuleFor(a => a.Password, f => f.Internet.Password())
                .RuleFor(a => a.PerfilType, f => (PerfilDto)account.PerfilType.Id)
                .Generate();

            return fakeAdministrativeAccountDto;
        }
    }

    public List<AdministrativeAccountDto> GetDtoListFromAdministrativeAccountList(IList<AdministrativeAccount> accounts)
    {
        lock (LockObject)
        {
            var administrativeAccountDtoList = new List<AdministrativeAccountDto>();

            foreach (var account in accounts)
            {
                var accountDto = GetNewDtoFromAdministrativeAccount(account);
                administrativeAccountDtoList.Add(accountDto);
            }

            return administrativeAccountDtoList;
        }
    }
}
