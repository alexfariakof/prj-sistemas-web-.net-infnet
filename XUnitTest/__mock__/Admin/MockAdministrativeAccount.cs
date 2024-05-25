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

    public AdministrativeAccountDto GetFakerDto()
    {
        lock (LockObject)
        {
            var fakeAdministrativeAccountDto = new Faker<AdministrativeAccountDto>()
                .RuleFor(a => a.Id, f => f.Random.Guid())
                .RuleFor(a => a.Name, f => f.Name.FirstName())
                .RuleFor(a => a.Email, f => f.Internet.Email())
                .RuleFor(a => a.Password, f => f.Internet.Password())
                .RuleFor(a => a.PerfilType, f => (PerfilDto)Perfil.UserType.Admin)
                .Generate();
            fakeAdministrativeAccountDto.UsuarioId = fakeAdministrativeAccountDto.Id;
            return fakeAdministrativeAccountDto;
        }
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
    
    public AdministrativeAccountDto GetFakerDto(AdministrativeAccount account = null)
    {
        lock (LockObject)
        {
            if (account == null) account = MockAdministrativeAccount.Instance.GetFaker();

            var fakeAdministrativeAccountDto = new Faker<AdministrativeAccountDto>()
                .RuleFor(a => a.Id, f => account.Id)
                .RuleFor(a => a.UsuarioId, f => account.Id)
                .RuleFor(a => a.Name, f => account.Name)
                .RuleFor(a => a.Email, f => account.Login.Email)
                .RuleFor(a => a.Password, f => f.Internet.Password())
                .RuleFor(a => a.PerfilType, f => (PerfilDto)account.PerfilType.Id)
                .Generate();

            return fakeAdministrativeAccountDto;
        }
    }

    public List<AdministrativeAccountDto> GetFakerListDto(IList<AdministrativeAccount> accounts = null, int count = 3)
    {
        lock (LockObject)
        {
            if (accounts == null) accounts = MockAdministrativeAccount.Instance.GetListFaker(count);

            var administrativeAccountDtoList = new List<AdministrativeAccountDto>();

            foreach (var account in accounts)
            {
                var accountDto = GetFakerDto(account);
                administrativeAccountDtoList.Add(accountDto);
            }

            return administrativeAccountDtoList;
        }
    }
}
