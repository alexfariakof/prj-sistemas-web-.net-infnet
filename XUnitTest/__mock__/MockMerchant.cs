using Domain.Account.Agreggates;
using Domain.Transactions.Agreggates;
using Bogus;
using Bogus.Extensions.Brazil;
using Domain.Notifications;
using Domain.Account.ValueObject;
using Application.Account.Dto;

namespace __mock__;
public static class MockMerchant
{
    public static Merchant GetFaker()
    {
        var fakeMerchant = new Faker<Merchant>()
            .RuleFor(m => m.Id, f => f.Random.Guid())
            .RuleFor(m => m.Name, f => f.Name.FirstName())
            .RuleFor(m => m.Login, MockLogin.GetFaker())
            .RuleFor(m => m.CPF, f => f.Person.Cpf())
            .RuleFor(m => m.CNPJ, f => f.Company.Cnpj())
            .RuleFor(c => c.Phone, f => new Phone { Number = f.Person.Phone })
            .RuleFor(c => c.Addresses, MockAddress.GetListFaker(1))
            .RuleFor(m => m.Cards, f => new List<Card>())
            .RuleFor(m => m.Signatures, f => new List<Signature>())
            .RuleFor(m => m.Notifications, f => new List<Notification>())
            .Generate();

        return fakeMerchant;
    }
    public static List<Merchant> GetListFaker(int count)
    {
        var merchantList = new List<Merchant>();
        for (var i = 0; i < count; i++)
        {
            merchantList.Add(GetFaker());
        }
        return merchantList;
    }
    public static MerchantDto GetDtoFromMerchant(Merchant merchant)
    {
        var fakeMerchantDto = new Faker<MerchantDto>()
            .RuleFor(c => c.Id, f => merchant.Id)
            .RuleFor(c => c.Name, f => merchant.Name)
            .RuleFor(c => c.Email, f => merchant.Login.Email)
            .RuleFor(c => c.Password, f => merchant.Login.Password)
            .RuleFor(c => c.CPF, f => merchant.CPF)
            .RuleFor(c => c.CNPJ, f => merchant.CNPJ)
            .RuleFor(c => c.Phone, f => merchant.Phone.Number)
            .RuleFor(c => c.Address, f => MockAddress.GetDtoFromAddress(merchant.Addresses[0]))
            .RuleFor(c => c.FlatId, f => Guid.NewGuid())
            .Generate();

        return fakeMerchantDto;
    }
    public static List<MerchantDto> GetDtoListFromMerchantList(List<Merchant> merchants)
    {
        var merchantDtoList = new List<MerchantDto>();

        foreach (var merchant in merchants)
        {
            var merchantDto = GetDtoFromMerchant(merchant);
            merchantDtoList.Add(merchantDto);
        }

        return merchantDtoList;
    }
}