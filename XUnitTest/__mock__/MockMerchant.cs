using Domain.Account.Agreggates;
using Domain.Transactions.Agreggates;
using Bogus;
using Bogus.Extensions.Brazil;
using Domain.Notifications;

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
}